using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _service;

        public UserSettingsController(IUserSettingsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var settings = await _service.GetAllAsync();
            return Ok(settings);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _service.GetByIdAsync(userId);
            if (result == null)
                return NotFound($"No settings found for user with ID: {userId}");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSettings settings)
        {
            var created = await _service.AddAsync(settings);
            return Ok(created); // Không dùng CreatedAtAction nếu không có Id độc lập
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, UserSettings settings)
        {
            if (userId != settings.UserId)
                return BadRequest("UserId in URL and body do not match.");

            var updated = await _service.UpdateAsync(settings);
            return Ok(updated);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var success = await _service.DeleteAsync(userId);
            if (!success)
                return NotFound($"UserSettings not found for userId {userId}");

            return NoContent();
        }
    }
}
