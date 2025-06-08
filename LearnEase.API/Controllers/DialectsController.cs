using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DialectsController : ControllerBase
    {
        private readonly IDialectService _service;
        public DialectsController(IDialectService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Dialect dialect)
        {
            var created = await _service.AddAsync(dialect);
            return CreatedAtAction(nameof(GetById), new { id = created.DialectId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Dialect dialect)
        {
            if (id != dialect.DialectId) return BadRequest();
            var updated = await _service.UpdateAsync(dialect);
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
