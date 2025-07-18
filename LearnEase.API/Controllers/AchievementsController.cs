﻿using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _service;

        public AchievementsController(IAchievementService service)
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
        public async Task<IActionResult> Create(Achievement achievement)
        {
            var created = await _service.AddAsync(achievement);
            return CreatedAtAction(nameof(GetById), new { id = created.AchievementId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Achievement achievement)
        {
            if (id != achievement.AchievementId) return BadRequest();
            var updated = await _service.UpdateAsync(achievement);
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
