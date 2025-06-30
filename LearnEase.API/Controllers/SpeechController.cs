using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;
using LearnEase.Repository.IRepo;
using LearnEase.Repository;

namespace LearnEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : Controller
    {
        private readonly ILeaderboardRepository _leaderboardRepo;
        private readonly IGenericRepository<UserLessonAttempt> _attemptRepo;
        private readonly IGenericRepository<AiLessonPart> _partRepo;
        private readonly IUnitOfWork _uow;

        public SpeechController(
            ILeaderboardRepository leaderboardRepo,
            IGenericRepository<UserLessonAttempt> attemptRepo,
            IGenericRepository<AiLessonPart> partRepo,
            IUnitOfWork uow)
        {
            _leaderboardRepo = leaderboardRepo;
            _attemptRepo = attemptRepo;
            _partRepo = partRepo;
            _uow = uow;
        }

        private const string GoogleApiKey = "AIzaSyCqD2UcgaH3zn8Z94ZbBhc1O_9ZV_83FX8";
        private const string GoogleApiUrl = $"https://speech.googleapis.com/v1/speech:recognize?key={GoogleApiKey}";

        [HttpPost("transcribe")]
        public async Task<IActionResult> TranscribeAudio(
            IFormFile audioFile,
            [FromQuery] Guid userId,
            [FromQuery] Guid lessonId)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("File không hợp lệ.");

            var part = (await _partRepo.GetAllAsync())
                .FirstOrDefault(p => p.LessonId == lessonId && p.Skill == SkillType.Speaking);

            if (part == null)
                return NotFound("❌ Không tìm thấy phần Speaking của bài học.");

            byte[] audioBytes;
            using (var memoryStream = new MemoryStream())
            {
                await audioFile.CopyToAsync(memoryStream);
                audioBytes = memoryStream.ToArray();
            }

            var base64Audio = Convert.ToBase64String(audioBytes);

            var requestPayload = new
            {
                config = new
                {
                    encoding = "MP3",
                    sampleRateHertz = 16000,
                    languageCode = "en-GB"
                },
                audio = new
                {
                    content = base64Audio
                }
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(GoogleApiUrl, content);
            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);
            string transcript = doc.RootElement
                .GetProperty("results")[0]
                .GetProperty("alternatives")[0]
                .GetProperty("transcript")
                .GetString();

            // So sánh với prompt (hoặc ReferenceText nếu có)
            string expected = (part.ReferenceText ?? part.Prompt).Trim().ToLower();
            string actual = transcript?.Trim().ToLower() ?? "";
            double similarity = ComputeSimilarity(expected, actual);
            float score = (float)Math.Round(similarity * 100);

            string feedback = score switch
            {
                >= 95 => "🎯 Bạn phát âm rất chuẩn!",
                >= 80 => "👍 Khá ổn, chỉ vài chỗ nhỏ cần cải thiện.",
                >= 60 => "📝 Phát âm cần luyện tập thêm.",
                _ => "⚠️ Bạn cần nói rõ hơn để cải thiện điểm."
            };

            // Lưu vào Attempt
            var attempt = new UserLessonAttempt
            {
                AttemptId = Guid.NewGuid(),
                UserId = userId,
                LessonId = lessonId,
                Skill = SkillType.Speaking,
                UserAnswer = transcript,
                Score = score,
                Feedback = feedback,
                AttemptedAt = DateTime.UtcNow
            };

            await _attemptRepo.AddAsync(attempt);

            // Ghi điểm leaderboard
            int rounded = (int)Math.Round(score);
            foreach (var period in new[] { "weekly", "monthly" })
            {
                await _leaderboardRepo.RecordScoreAsync(new RecordScoreDto
                {
                    UserId = userId,
                    Score = rounded,
                    Period = period
                });
            }

            await _uow.SaveAsync();

            return Ok(new
            {
                transcript,
                score,
                feedback
            });
        }

        private double ComputeSimilarity(string expected, string actual)
        {
            var expectedWords = expected.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var actualWords = actual.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int match = 0;
            int len = Math.Max(expectedWords.Length, actualWords.Length);
            for (int i = 0; i < Math.Min(expectedWords.Length, actualWords.Length); i++)
            {
                if (expectedWords[i].Equals(actualWords[i], StringComparison.OrdinalIgnoreCase))
                    match++;
            }

            return len == 0 ? 0 : (double)match / len;
        }
    }
}
