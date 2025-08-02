using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using LearnEase.Service.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [ApiController]
    [Route("api/learning")]
    public class LearningController : ControllerBase
    {
        private readonly ILearningService _learningService;

        public LearningController(ILearningService learningService)
        {
            _learningService = learningService;
        }

        [HttpGet("next-lesson")]
        public async Task<IActionResult> GetNextLessonForUser()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized("Không xác định được người dùng.");

            var nextLesson = await _learningService.GetNextLessonForUserAsync(userId);
            if (nextLesson == null)
                return NotFound("Không còn bài học nào hoặc bạn đã hoàn thành tất cả.");

            return Ok(nextLesson);
        }

        [HttpGet("next-lesson-block")]
        public async Task<IActionResult> GetNextLessonBlock([FromQuery] Guid userId)
        {
            var lessonBlock = await _learningService.GetNextLessonBlockForUserAsync(userId);
            if (lessonBlock == null)
                return NotFound("Không tìm thấy bài học tiếp theo.");

            return Ok(lessonBlock);
        }

        [HttpGet("completed-lessons/{topicId}")]
        public async Task<IActionResult> GetCompletedLessonsCount(Guid topicId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized("Không xác định được người dùng.");

            int count = await _learningService.GetCompletedLessonCountInTopic(userId, topicId);
            return Ok(new { topicId, completedLessons = count });
        }

        [HttpPost("update-topic-progress/{topicId}")]
        public async Task<IActionResult> UpdateTopicProgress(Guid topicId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized("Không xác định được người dùng.");

            await _learningService.UpdateTopicProgress(userId, topicId);
            return Ok(new { message = "Cập nhật tiến độ chủ đề thành công." });
        }

        [HttpGet("lesson-block")]
        public async Task<IActionResult> GetLessonBlock([FromQuery] Guid userId, [FromQuery] Guid lessonId)
        {
            if (userId == Guid.Empty || lessonId == Guid.Empty)
                return BadRequest("Thiếu userId hoặc lessonId.");

            var lessonBlock = await _learningService.GetLessonBlockByUserAndLessonAsync(userId, lessonId);
            if (lessonBlock == null)
                return NotFound("Không tìm thấy bài học phù hợp với người dùng.");

            return Ok(lessonBlock);
        }

        [AllowAnonymous]
        [HttpPost("submit-progress")]
        public async Task<IActionResult> SubmitProgress([FromQuery] Guid userId, [FromBody] SubmitProgressRequest request)
        {
            if (userId == Guid.Empty)
                return Unauthorized("Không xác định được người dùng.");

            if (request.LessonId == Guid.Empty || (request.VocabId == null && request.ExerciseId == null))
                return BadRequest("Thiếu lessonId hoặc mục từ cần ghi nhận tiến độ.");

            bool result = await _learningService.SubmitUserProgressAsync(
                userId,
                request.LessonId,
                request.VocabId,
                request.ExerciseId,
                request.IsCorrect
            );

            return result
                ? Ok(new { message = "Đã lưu tiến độ học tập." })
                : StatusCode(500, "Lỗi trong quá trình lưu tiến độ.");
        }
    }
}
