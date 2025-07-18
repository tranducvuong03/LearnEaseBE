using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.Api.Controllers
{
    [Route("api/ai")]
    [ApiController]
    [AllowAnonymous]
    public class AIController : ControllerBase
    {
        private readonly IOpenAIService _aiService;
        private readonly ILogger<AIController> _logger;
        private readonly List<object> _conversationHistory = new List<object>();
        public AIController(IOpenAIService aiService, ILogger<AIController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskAI([FromBody] string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return BadRequest("⚠️ Vui lòng nhập câu hỏi!");
            }

            try
            {
                _logger.LogInformation($"AI request received: {userInput}");

                // Tên mặc định hoặc lấy từ context nếu có đăng nhập
                string username = "bạn";

                string response = await _aiService.GetAIResponseAsync(
                    userInput,
                    useDatabase: false,
                    conversationHistory: _conversationHistory,
                    username: username
                );

                _conversationHistory.Add(new { role = "user", content = userInput });
                _conversationHistory.Add(new { role = "assistant", content = response });

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ AI processing error: {ex.Message}");
                return StatusCode(500, "⚠️ Đã xảy ra lỗi hệ thống, vui lòng thử lại sau!");
            }
        }
    }
}
