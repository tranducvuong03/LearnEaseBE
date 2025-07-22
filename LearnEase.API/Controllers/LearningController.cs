using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using LearnEase.Service.Models.Response;

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
                return Unauthorized();

            NextLesson nextLesson = await _learningService.GetNextLessonForUserAsync(userId);
            if (nextLesson == null)
                return NotFound("No more lessons available for this user or you have completed all lessons.");

            return Ok(nextLesson);
        }

        [HttpGet("next-lesson-block")]
        public async Task<IActionResult> GetNextLessonBlock([FromQuery] Guid userId)
        {
            var lessonBlock = await _learningService.GetNextLessonBlockForUserAsync(userId);
            if (lessonBlock == null)
                return NotFound();

            return Ok(lessonBlock);
        }

        [HttpGet("completed-lessons/{topicId}")]
        public async Task<IActionResult> GetCompletedLessonsCount(Guid topicId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            int count = await _learningService.GetCompletedLessonCountInTopic(userId, topicId);
            return Ok(new { topicId, completedLessons = count });
        }

        [HttpPost("update-topic-progress/{topicId}")]
        public async Task<IActionResult> UpdateTopicProgress(Guid topicId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            await _learningService.UpdateTopicProgress(userId, topicId);
            return Ok(new { message = "Topic progress updated successfully." });
        }

        [HttpGet("lesson-block")]
        public async Task<IActionResult> GetLessonBlock([FromQuery] Guid userId, [FromQuery] Guid lessonId)
        {
            if (userId == Guid.Empty || lessonId == Guid.Empty)
                return BadRequest("Missing userId or lessonId.");

            var lessonBlock = await _learningService.GetLessonBlockByUserAndLessonAsync(userId, lessonId);
            if (lessonBlock == null)
                return NotFound("Không tìm thấy bài học phù hợp với người dùng.");

            return Ok(lessonBlock);
        }
    }
}
