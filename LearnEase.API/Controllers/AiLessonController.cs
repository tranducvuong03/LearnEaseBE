using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Repository;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Service.Models.Request;
using System.Text.Json;
using LearnEase.Repository.DTO;
using LearnEase.Repository.IRepo;
using LearnEase.Service.Services;
using System.Text.RegularExpressions;

namespace LearnEase.API.Controllers
{
    [Route("api/ai-lesson")]
    [ApiController]
    public class AiLessonController : ControllerBase
    {
        private readonly IAiLessonService _lessonService;
        private readonly IGenericRepository<AiLesson> _lessonRepo;
        private readonly IGenericRepository<AiLessonPart> _partRepo;
        private readonly IGenericRepository<UserLessonAttempt> _attemptRepo;
        private readonly IUnitOfWork _uow;
        private readonly ILeaderboardRepository _leaderboardRepo;
        private readonly IOpenAIService _openAIService;

        public AiLessonController(
           IAiLessonService lessonService,
           IUnitOfWork uow,
           ILeaderboardRepository leaderboardRepo,
           IOpenAIService openAIService // 👈 thêm tham số này
       )
        {
            _uow = uow;
            _lessonService = lessonService;
            _lessonRepo = uow.GetRepository<AiLesson>();
            _partRepo = uow.GetRepository<AiLessonPart>();
            _attemptRepo = uow.GetRepository<UserLessonAttempt>();
            _leaderboardRepo = leaderboardRepo; // 👈 đừng quên dòng này
            _openAIService = openAIService;     // ✅ fix null tại đây
        }

        /// <summary>
        /// Sinh một bài học mới với 4 kỹ năng bằng AI.
        /// </summary>
        /// <param name="topic">Chủ đề bài học (VD: Travel, Food,...)</param>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateLesson([FromQuery] string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return BadRequest("❌ Chủ đề không được để trống.");

            var lesson = await _lessonService.GenerateLessonAsync(topic);
            return Ok(new { lesson.LessonId, lesson.Topic, lesson.CreatedAt });
        }

        /// <summary>
        /// Lấy thông tin chi tiết bài học (bao gồm cả các phần Listening/Speaking/Reading/Writing).
        /// </summary>
        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetLessonDetails(Guid lessonId)
        {
            var lesson = await _lessonRepo.GetByIdAsync(lessonId);
            if (lesson == null)
                return NotFound("❌ Không tìm thấy bài học.");

            var parts = (await _partRepo.GetAllAsync())
                        .Where(p => p.LessonId == lessonId)
                        .Select(p => new
                        {
                            skill = p.Skill.ToString(),
                            prompt = p.Prompt,
                            referenceText = p.ReferenceText,
                            audioUrl = p.AudioUrl,
                            choicesJson = p.ChoicesJson
                        });

            return Ok(new
            {
                lesson.LessonId,
                lesson.Topic,
                lesson.CreatedAt,
                parts = parts
            });
        }
        private bool IsLessonAvailableToday(AiLesson lesson)
        {
            int todayIndex = (int)DateTime.UtcNow.DayOfWeek;
            if (todayIndex == 0) todayIndex = 7;
            return lesson.DayIndex == todayIndex;
        }
        [HttpGet("weekly-lessons")]
        public async Task<IActionResult> GetWeeklyLessons()
        {
            var lessons = await _lessonService.GetOrGenerateWeeklyLessonsAsync();
            var parts = await _partRepo.GetAllAsync();

            var response = lessons
                .OrderBy(l => l.DayIndex)
                .Select(lesson => new
                {
                    lesson.LessonId,
                    lesson.Topic,
                    lesson.DayIndex,
                    lesson.CreatedAt,
                    Parts = parts
                        .Where(p => p.LessonId == lesson.LessonId)
                        .Select(p => new
                        {
                            skill = p.Skill.ToString(),
                            prompt = p.Prompt,
                            referenceText = p.ReferenceText,
                            audioUrl = p.AudioUrl,
                            choicesJson = p.ChoicesJson
                        })
                });

            return Ok(response);
        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> EvaluateLessonAnswers([FromBody] EvaluateLessonRequest request)

        {
            var lesson = await _lessonRepo.GetByIdAsync(request.LessonId);
            if (lesson == null) return BadRequest("❌ Bài học không tồn tại.");
            if (!IsLessonAvailableToday(lesson)) return BadRequest("⏳ Bạn chỉ có thể làm bài học của ngày hôm nay.");

            var existingAttempt = (await _attemptRepo.GetAllAsync())
.FirstOrDefault(a => a.UserId == request.UserId && a.LessonId == request.LessonId && a.Skill == request.Skill);

            if (existingAttempt != null)
                return BadRequest("❌ Hôm nay bạn đã thử thách này rồi.");

            var part = (await _partRepo.GetAllAsync())
                        .FirstOrDefault(p => p.LessonId == request.LessonId && p.Skill == request.Skill);

            if (part == null || string.IsNullOrEmpty(part.ChoicesJson))
                return BadRequest("❌ Không tìm thấy bài học hoặc phần kỹ năng phù hợp để chấm điểm.");

            var correctList = JsonSerializer.Deserialize<List<ChoiceQuestion>>(part.ChoicesJson);
            int total = correctList.Count;
            int correct = 0;

            foreach (var index in request.Answers.Keys)
            {
                if (int.TryParse(index, out int i) && i < total)
                {
                    if (request.Answers[index] == correctList[i].Answer)
                        correct++;
                }
            }
            string aiFeedback = await _openAIService.GenerateQuizFeedbackAsync(correct, total);

            float score = (100f / total) * correct;

            var attempt = new UserLessonAttempt
            {
                AttemptId = Guid.NewGuid(),
                UserId = request.UserId,
                LessonId = request.LessonId,
                Skill = request.Skill,
                UserAnswer = JsonSerializer.Serialize(request.Answers),
                Score = score,
                Feedback = aiFeedback,
                AttemptedAt = DateTime.UtcNow
            };
            var periods = new[] { "weekly", "monthly" };
            int rounded = (int)Math.Round(score);

            foreach (var p in periods)
            {
                await _leaderboardRepo.RecordScoreAsync(new RecordScoreDto
                {
                    UserId = request.UserId,
                    Score = rounded,
                    Period = p
                });
            }

            await _attemptRepo.AddAsync(attempt);
            await _uow.SaveAsync();

            return Ok(new
            {
                score,
                correct,
                total,
                feedback = attempt.Feedback
            });
        }
        [HttpGet("attempts")]
        public async Task<IActionResult> GetUserAttempt(
    [FromQuery] Guid userId,
    [FromQuery] Guid lessonId,
    [FromQuery] SkillType skill)
        {
            var attempt = (await _attemptRepo.GetAllAsync())
                .Where(a => a.UserId == userId && a.LessonId == lessonId && a.Skill == skill)
                .OrderByDescending(a => a.AttemptedAt)
                .FirstOrDefault();

            if (attempt == null)
                return NotFound("❌ Không tìm thấy kết quả cho kỹ năng này.");

            var parsedAnswers = string.IsNullOrEmpty(attempt.UserAnswer)
                ? null
                : JsonSerializer.Deserialize<Dictionary<string, string>>(attempt.UserAnswer);

            return Ok(new
            {
                attempt.UserId,
                attempt.LessonId,
                skill = attempt.Skill.ToString(),
                attempt.Score,
                attempt.Feedback,
                userAnswer = parsedAnswers,
                attempt.AttemptedAt
            });
        }
        [HttpGet("review")]
        public async Task<IActionResult> ReviewLessonResult(
            [FromQuery] Guid userId,
            [FromQuery] Guid lessonId,
            [FromQuery] SkillType skill)
        {
            var part = (await _partRepo.GetAllAsync())
               .FirstOrDefault(p => p.LessonId == lessonId && p.Skill.ToString() == skill.ToString());

            var attempt = (await _attemptRepo.GetAllAsync())
                .FirstOrDefault(a => a.UserId == userId && a.LessonId == lessonId && a.Skill == skill);

            if (part == null || attempt == null)
                return NotFound("❌ Không đủ dữ liệu để hiển thị bài làm.");

            var response = new Dictionary<string, object>
            {
                ["skill"] = skill.ToString(),
                ["prompt"] = part.Prompt,
                ["score"] = attempt.Score,
                ["feedback"] = attempt.Feedback,
                ["attemptedAt"] = attempt.AttemptedAt
            };

            switch (skill)
            {
                case SkillType.Reading:
                    if (!string.IsNullOrEmpty(part.ChoicesJson))
                    {
                        var questions = JsonSerializer.Deserialize<List<ChoiceQuestion>>(part.ChoicesJson);
                        var userAnswers = JsonSerializer.Deserialize<Dictionary<string, string>>(attempt.UserAnswer);

                        var merged = questions.Select((q, i) => new
                        {
                            question = q.Question,
                            choices = q.Choices,
                            correct = q.Answer,
                            userAnswer = userAnswers.ContainsKey(i.ToString()) ? userAnswers[i.ToString()] : null
                        });

                        response["referenceText"] = part.ReferenceText;
                        response["questions"] = merged;
                    }
                    break;

                case SkillType.Writing:
                case SkillType.Speaking:
                case SkillType.Listening:
                    response["userAnswer"] = attempt.UserAnswer;
                    response["referenceText"] = part.ReferenceText;
                    response["audioUrl"] = part.AudioUrl;
                    break;
            }

            return Ok(response);
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyLesson()
        {
            var lesson = await _lessonService.GetCurrentWeeklyLessonAsync();
            if (lesson == null)
                return Ok(new { message = "⚠️ Tuần này hệ thống chưa có bài thi đua nào. Vui lòng thử lại sau." });

            var parts = (await _partRepo.GetAllAsync())
                .Where(p => p.LessonId == lesson.LessonId)
                .Select(p => new
                {
                    skill = p.Skill.ToString(),
                    prompt = p.Prompt,
                    referenceText = p.ReferenceText,
                    audioUrl = p.AudioUrl,
                    choicesJson = p.ChoicesJson
                });

            return Ok(new
            {
                lesson.LessonId,
                lesson.Topic,
                lesson.CreatedAt,
                parts = parts
            });
        }
        [HttpGet("explanation")]
        public async Task<IActionResult> GetExplanationForAnswers(
    [FromQuery] Guid userId,
    [FromQuery] Guid lessonId,
    [FromQuery] SkillType skill)
        {
            var attempt = (await _attemptRepo.GetAllAsync())
                .FirstOrDefault(a => a.UserId == userId && a.LessonId == lessonId && a.Skill == skill);

            var part = (await _partRepo.GetAllAsync())
                .FirstOrDefault(p => p.LessonId == lessonId && p.Skill == skill);

            if (attempt == null || part == null || string.IsNullOrEmpty(part.ChoicesJson))
                return NotFound("Không đủ dữ liệu để giải thích.");

            var questions = JsonSerializer.Deserialize<List<ChoiceQuestion>>(part.ChoicesJson);
            var userAnswers = JsonSerializer.Deserialize<Dictionary<string, string>>(attempt.UserAnswer ?? "{}");

            var explainList = new List<object>();

            for (int i = 0; i < questions.Count; i++)
            {
                string user = userAnswers.ContainsKey(i.ToString()) ? userAnswers[i.ToString()] : "(không chọn)";
                string correct = questions[i].Answer;
                string question = questions[i].Question;
                var choices = questions[i].Choices;

                if (user == correct)
                {
                    explainList.Add(new
                    {
                        questionNumber = $"Câu {i + 1}",
                        question,
                        correct,
                        userAnswer = user,
                        explanation = "✅ Bạn đã chọn đúng. Tuyệt vời! 🎉"
                    });
                    continue;
                }


                string prompt = $@"
Bạn là giáo viên tiếng Anh. Hãy giải thích câu trắc nghiệm sau cho học viên tiếng Việt:

Câu hỏi: {question}
Các lựa chọn: {string.Join(", ", choices)}
Học viên đã chọn: {user}
Đáp án đúng: {correct}

👉 Yêu cầu:
- Giải thích vì sao đáp án đúng là {correct}.
- Nêu rõ keyword hoặc cụm từ khóa chính giúp phân biệt.
- Có thể chỉ ra lỗi sai của học viên nếu có.
- Viết bằng tiếng Việt, ngắn gọn, tối đa 3–4 câu.

Trả lời trực tiếp, không cần tiêu đề hay định dạng.";

                string explanation = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");

                explainList.Add(new
                {
                    question,
                    correct,
                    userAnswer = user,
                    explanation = explanation.Trim()
                });
            }

            return Ok(new { explanations = explainList });
        }
        [HttpPost("evaluate-writing")]
        public async Task<IActionResult> EvaluateWriting([FromBody] EvaluateLessonRequest request)
        {
            var existingAttempt = (await _attemptRepo.GetAllAsync())
   .FirstOrDefault(a => a.UserId == request.UserId && a.LessonId == request.LessonId && a.Skill == request.Skill);

            if (existingAttempt != null)
                return BadRequest("❌ Hôm nay bạn đã thử thách này rồi.");
            var part = (await _partRepo.GetAllAsync())
                        .FirstOrDefault(p => p.LessonId == request.LessonId && p.Skill == SkillType.Writing);

            if (part == null)
                return BadRequest("❌ Không tìm thấy phần viết để chấm điểm.");

            string userWriting = request.Answers?.Values.FirstOrDefault()?.Trim();
            if (string.IsNullOrWhiteSpace(userWriting))
                return BadRequest("❌ Bài viết không được để trống.");

            int wordCount = userWriting.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (wordCount < 8)
            {
                var tooShortAttempt = new UserLessonAttempt
                {
                    AttemptId = Guid.NewGuid(),
                    UserId = request.UserId,
                    LessonId = request.LessonId,
                    Skill = SkillType.Writing,
                    UserAnswer = JsonSerializer.Serialize(request.Answers),
                    Score = 0,
                    Feedback = "Bài viết quá ngắn. Điểm: 0",
                    AttemptedAt = DateTime.UtcNow
                };

                await _attemptRepo.AddAsync(tooShortAttempt);
                await _uow.SaveAsync();

                return Ok(new { score = 0, feedback = tooShortAttempt.Feedback });
            }

            string prompt = $@"
Bạn là giáo viên tiếng Anh, nhiệm vụ của bạn là chấm điểm bài viết của học viên.

Đề bài: {part.Prompt}

Bài viết của học viên:
{userWriting}

👉 Yêu cầu:
- Kiểm tra xem học viên có viết đúng chủ đề không. Nếu lạc đề, cho điểm 0.
- Nếu đúng chủ đề thì:
  - Phân tích và tìm các lỗi ngữ pháp, từ vựng, chính tả hoặc cấu trúc.
  - Mỗi lỗi trừ 3 điểm, bắt đầu từ 100.
  - Viết nhận xét ngắn gọn (3-4 câu) bằng tiếng Việt.
  - Ghi rõ tổng số lỗi và điểm còn lại. Ví dụ: Lỗi: 7 | Điểm: 79
- Trả về một đoạn đánh giá duy nhất, không cần định dạng markdown.
";

            string feedback = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
            float score = ExtractScoreFromFeedback(feedback);  // vẫn giữ logic cũ

            var attempt = new UserLessonAttempt
            {
                AttemptId = Guid.NewGuid(),
                UserId = request.UserId,
                LessonId = request.LessonId,
                Skill = SkillType.Writing,
                UserAnswer = JsonSerializer.Serialize(request.Answers),
                Score = score,
                Feedback = feedback,
                AttemptedAt = DateTime.UtcNow
            };

            await _attemptRepo.AddAsync(attempt);
            await _uow.SaveAsync();

            int rounded = (int)Math.Round(score);
            var periods = new[] { "weekly", "monthly" };
            foreach (var p in periods)
            {
                await _leaderboardRepo.RecordScoreAsync(new RecordScoreDto
                {
                    UserId = request.UserId,
                    Score = rounded,
                    Period = p
                });
            }

            return Ok(new { score, feedback });
        }

      
     private float ExtractScoreFromFeedback(string feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback))
                return 0;

            // 1️⃣ Bắt "Điểm là 94", "Điểm: 94", "với điểm là 94", "Điểm còn lại là 76"
            var match = Regex.Match(feedback, @"(?i)điểm(?:\s*(?:là|:|：))?\s*(\d{1,3})");

            if (match.Success && int.TryParse(match.Groups[1].Value, out var score))
                return Math.Min(score, 100);  // Clamp về tối đa 100

            // 2️⃣ Fallback: tìm số cuối cùng nếu regex thất bại
            var matches = Regex.Matches(feedback, @"\d{1,3}");
            if (matches.Count > 0 && int.TryParse(matches[^1].Value, out var fallbackScore))
                return Math.Min(fallbackScore, 100);

            return 0f;
        }

        [HttpPost("evaluate-listening")]
        public async Task<IActionResult> EvaluateListening([FromBody] EvaluateLessonRequest request)
        {

            var existingAttempt = (await _attemptRepo.GetAllAsync())
    .FirstOrDefault(a => a.UserId == request.UserId && a.LessonId == request.LessonId && a.Skill == request.Skill);

            if (existingAttempt != null)
                return BadRequest("❌ Hôm nay bạn đã thử thách này rồi.");
            var part = (await _partRepo.GetAllAsync())
                        .FirstOrDefault(p => p.LessonId == request.LessonId && p.Skill == SkillType.Listening);

            if (part == null || string.IsNullOrWhiteSpace(part.ReferenceText))
                return BadRequest("❌ Không tìm thấy đoạn audio để chấm điểm.");

            string userText = request.Answers?.Values.FirstOrDefault()?.Trim().ToLower() ?? "";
            string expected = part.ReferenceText.Trim().ToLower();

            if (string.IsNullOrEmpty(userText))
                return BadRequest("❌ Bạn chưa nhập câu trả lời.");

            double similarity = ComputeSimilarity(expected, userText);
            float score = (float)Math.Round(similarity * 100);

            string feedback = score switch
            {
                >= 95 => "🎯 Tuyệt vời! Bạn nghe và viết rất chính xác.",
                >= 80 => "👍 Bạn làm tốt. Còn vài lỗi nhỏ.",
                >= 60 => "📝 Cần cải thiện. Có nhiều điểm bạn nghe chưa đúng.",
                _ => "⚠️ Bạn cần luyện nghe thêm. Hãy thử lại."
            };

            var attempt = new UserLessonAttempt
            {
                AttemptId = Guid.NewGuid(),
                UserId = request.UserId,
                LessonId = request.LessonId,
                Skill = SkillType.Listening,
                UserAnswer = JsonSerializer.Serialize(request.Answers),
                Score = score,
                Feedback = feedback,
                AttemptedAt = DateTime.UtcNow
            };

            await _attemptRepo.AddAsync(attempt);
            await _uow.SaveAsync();
            int rounded = (int)Math.Round(score);
            var periods = new[] { "weekly", "monthly" };
            foreach (var p in periods)
            {
                await _leaderboardRepo.RecordScoreAsync(new RecordScoreDto
                {
                    UserId = request.UserId,
                    Score = rounded,
                    Period = p
                });
            }

            return Ok(new { score, feedback });
        }
        private double ComputeSimilarity(string expected, string actual)
        {
            var expectedWords = expected.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var actualWords = actual.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int correctCount = 0;
            var matched = new HashSet<int>();

            foreach (var word in actualWords)
            {
                for (int i = 0; i < expectedWords.Length; i++)
                {
                    if (!matched.Contains(i) &&
                        string.Equals(expectedWords[i], word, StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                        matched.Add(i);
                        break;
                    }
                }
            }

            return expectedWords.Length == 0 ? 0 : (double)correctCount / expectedWords.Length;
        }


    }
}
