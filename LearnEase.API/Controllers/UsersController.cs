using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

		[HttpGet("me")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized("Token is missing user ID");

			if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
				return BadRequest("Invalid user ID in token");

			var user = await _service.GetByIdAsync(userId);
			return user == null ? NotFound("User not found") : Ok(user);
		}

		[HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _service.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            if (id != user.UserId) return BadRequest();
            var updated = await _service.UpdateAsync(user);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        //xem tiến độ học tập
        [HttpGet("progress/{userId}")]
        public async Task<IActionResult> GetUserProgress(Guid userId)
        {
            var progress = await _service.GetByIdAsync(userId);
            if (progress == null) return NotFound("User progress not found");

            return Ok(progress);
        }
        [HttpPut("{id}/username")]
        public async Task<IActionResult> UpdateUsername(Guid id, [FromBody] UpdateUsernameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                return BadRequest("Username cannot be empty");

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.Username = request.Username;

            var updated = await _service.UpdateAsync(user);
            return Ok(updated);
        }
        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(Guid id, [FromBody] UpdateAvatarRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.AvatarUrl))
                return BadRequest("Avatar URL cannot be empty");
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");
            user.AvatarUrl = request.AvatarUrl;
            var updated = await _service.UpdateAsync(user);
            return Ok(updated);
        }


    }
}
