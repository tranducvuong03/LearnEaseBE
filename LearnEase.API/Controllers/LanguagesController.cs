using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _service;

        public LanguagesController(ILanguageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var language = await _service.GetByIdAsync(id);
            return language == null ? NotFound() : Ok(language);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            var created = await _service.AddAsync(language);
            return CreatedAtAction(nameof(GetById), new { id = created.LanguageId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Language language)
        {
            if (id != language.LanguageId) return BadRequest();
            var updated = await _service.UpdateAsync(language);
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
