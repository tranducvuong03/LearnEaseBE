using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
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
            {
                return Unauthorized();
            }

            NextLesson nextLesson = await _learningService.GetNextLessonForUserAsync(userId);

            if (nextLesson == null)
            {
                return NotFound("No more lessons available for this user or you have completed all lessons.");
            }

            return Ok(nextLesson);
        }

        [HttpGet("next-lesson-block")]
        public async Task<IActionResult> GetNextLessonBlock()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                return Unauthorized();

            var lessonBlock = await _learningService.GetNextLessonBlockForUserAsync(userId);
            if (lessonBlock == null)
                return NotFound();

            return Ok(lessonBlock);
        }

    }
}