using LearnEase.Service.IServices;
using LearnEase.Service.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("by-topic/{topicId}")]
        public async Task<IActionResult> GetLessonsByTopic(Guid topicId)
        {
            var lessons = await _lessonService.GetLessonsByTopicAsync(topicId);
            return Ok(lessons);
        }
    }
}
