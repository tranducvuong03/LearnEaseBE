using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VocabularyItemsController : ControllerBase
    {
        private readonly IVocabularyItemService _service;

        public VocabularyItemsController(IVocabularyItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VocabularyItem item)
        {
            var created = await _service.AddAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.VocabId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, VocabularyItem item)
        {
            if (id != item.VocabId) return BadRequest();
            var updated = await _service.UpdateAsync(item);
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
