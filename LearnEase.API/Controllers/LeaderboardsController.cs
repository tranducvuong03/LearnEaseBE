using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;
using LearnEase.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardsController : ControllerBase
    {
        private readonly ILeaderboardService _service;

        public LeaderboardsController(ILeaderboardService service)
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
        public async Task<IActionResult> Create(Leaderboard leaderboard)
        {
            var created = await _service.AddAsync(leaderboard);
            return CreatedAtAction(nameof(GetById), new { id = created.LeaderboardId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Leaderboard leaderboard)
        {
            if (id != leaderboard.LeaderboardId) return BadRequest();
            var updated = await _service.UpdateAsync(leaderboard);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
        // lấy điểm cao hàng tuần
        [HttpGet("leaderboard/top/week")]
        public async Task<IActionResult> GetTopUsers([FromQuery] string period = "weekly", [FromQuery] int top = 10)
        {
            var leaders = await _service.GetTopUserDisplayAsync(period, top);
            return Ok(leaders);
        }
        //hàng tháng 
        [HttpGet("leaderboard/top/month")]
        public async Task<IActionResult> GetTopUser([FromQuery] string period = "monthly", [FromQuery] int top = 10)
        {
            var leaders = await _service.GetTopUserDisplayAsync(period, top);
            return Ok(leaders);
        }
        //ghi điểm của game 
        [HttpPost("leaderboard/record")]
        public async Task<IActionResult> RecordScore([FromBody] RecordScoreDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.RecordScoreAsync(dto);
            return Ok("Score recorded successfully.");
        }
      
        }

}
