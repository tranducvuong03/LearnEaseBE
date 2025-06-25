using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Repository;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Service.Models.Request;
using System.Text.Json;
using LearnEase.Repository.DTO;
using LearnEase.Repository.IRepo;
using LearnEase.Service.Services;

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
        [HttpPost("evaluate")]
        public async Task<IActionResult> EvaluateLessonAnswers([FromBody] EvaluateLessonRequest request)
        {
           
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

    }
}
