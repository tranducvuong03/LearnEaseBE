using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _topicService.GetAllAsync();
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var topic = await _topicService.GetByIdAsync(id);
            if (topic == null) return NotFound();
            return Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Topic topic)
        {
            var created = await _topicService.AddAsync(topic);
            return CreatedAtAction(nameof(GetById), new { id = created.TopicId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Topic topic)
        {
            if (id != topic.TopicId) return BadRequest("ID mismatch");

            var existing = await _topicService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var updated = await _topicService.UpdateAsync(topic);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _topicService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("with-lessons")]
        public async Task<IActionResult> GetWithLessons()
        {
            var topics = await _topicService.GetWithLessonsAsync();
            return Ok(topics);
        }

        [HttpGet("with-progress/{userId}")]
        public async Task<IActionResult> GetWithProgress(Guid userId)
        {
            var topics = await _topicService.GetTopicsWithProgress(userId);
            return Ok(topics);
        }

    }
}
