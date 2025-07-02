using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.Iservice;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HandbagsController : ControllerBase
    {
        private readonly IHandbagService _service;
        public HandbagsController(IHandbagService service) => _service = service;

        [HttpGet]
        [Authorize(Roles = "administrator,moderator,developer,member")]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        [Authorize(Roles = "administrator,moderator,developer,member")]
        public IActionResult Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
                return NotFound(new { errorCode = "HB40401", message = "Not found" });
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "administrator,moderator")]
        public IActionResult Create([FromBody] Handbag handbag)
        {
            if (!Regex.IsMatch(handbag.ModelName, @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$"))
                return BadRequest(new { errorCode = "HB40001", message = "Invalid modelName format" });

            if (handbag.Price <= 0 || handbag.Stock <= 0)
                return BadRequest(new { errorCode = "HB40001", message = "Price/Stock must be > 0" });

            _service.Create(handbag);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "administrator,moderator")]
        public IActionResult Update(int id, [FromBody] Handbag handbag)
        {
            if (_service.GetById(id) == null)
                return NotFound(new { errorCode = "HB40401", message = "Not found" });

            handbag.Id = id;
            _service.Update(handbag);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "administrator,moderator")]
        public IActionResult Delete(int id)
        {
            if (_service.GetById(id) == null)
                return NotFound(new { errorCode = "HB40401", message = "Not found" });

            _service.Delete(id);
            return Ok();
        }

        [HttpGet("search")]
        [Authorize]
        public IActionResult Search([FromQuery] string modelName, string material)
        {
            return Ok(_service.Search(modelName, material));
        }
    }

}
