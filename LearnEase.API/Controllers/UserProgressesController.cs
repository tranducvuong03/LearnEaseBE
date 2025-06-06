using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserProgressesController : ControllerBase
    {
        private readonly IUserProgressService _service;

        public UserProgressesController(IUserProgressService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProgress progress)
        {
            var created = await _service.AddAsync(progress);
            return CreatedAtAction(nameof(GetById), new { id = created.ProgressId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserProgress progress)
        {
            if (id != progress.ProgressId) return BadRequest();
            var updated = await _service.UpdateAsync(progress);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}
