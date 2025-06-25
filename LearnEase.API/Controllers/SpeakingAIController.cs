using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;

namespace LearnEase.Api.Controllers
{
    [Route("api/speaking-ai")]
    [ApiController]
    public class SpeakingAIController : ControllerBase
    {
        private readonly IOpenAIService _aiService;
        private readonly ILogger<SpeakingAIController> _logger;

        public SpeakingAIController(IOpenAIService aiService, ILogger<SpeakingAIController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

      
        private const string GoogleApiKey = "AIzaSyCqD2UcgaH3zn8Z94ZbBhc1O_9ZV_83FX8"; 
        private const string GoogleApiUrl = $"https://speech.googleapis.com/v1/speech:recognize?key={GoogleApiKey}";

        [HttpGet("prompt")]
        public async Task<IActionResult> GeneratePrompt()
        {
            var prompt = "Give one English speaking prompt for learners. Max 15 words. Return only the sentence.";
            var generated = await _aiService.GetAIResponseAsync(prompt, false, new(), "System");

            return Ok(new { prompt = generated.Trim('"', '.', '\n') });
        }

        [HttpPost("evaluate")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EvaluateSpeaking([FromForm] EvaluateSpeakingRequest request)
        {
            if (request.AudioFile == null || string.IsNullOrWhiteSpace(request.OriginalPrompt))
                return BadRequest("Thiếu dữ liệu audio hoặc prompt.");

            var audioFile = request.AudioFile;
            var originalPrompt = request.OriginalPrompt;

            // Step 1: Convert audio to base64
            byte[] audioBytes;
            using (var memoryStream = new MemoryStream())
            {
                await audioFile.CopyToAsync(memoryStream);
                audioBytes = memoryStream.ToArray();
            }
            var base64Audio = Convert.ToBase64String(audioBytes);

            var googleRequest = new
            {
                config = new
                {
                    encoding = "MP3",
                    sampleRateHertz = 16000,
                    languageCode = "en-GB"
                },
                audio = new
                {
                    content = base64Audio
                }
            };

            using var httpClient = new HttpClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(googleRequest), Encoding.UTF8, "application/json");
            var googleResponse = await httpClient.PostAsync(GoogleApiUrl, jsonContent);
            var json = await googleResponse.Content.ReadAsStringAsync();

            JsonDocument parsed;
            try
            {
                parsed = JsonDocument.Parse(json);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi parse JSON từ Google: {ex.Message}");
                return StatusCode(500, "❌ Không thể phân tích phản hồi từ Google.");
            }

            // Step 2: Extract transcript safely
            string transcript = "";
            if (parsed.RootElement.TryGetProperty("results", out var resultsElement) &&
                resultsElement.GetArrayLength() > 0 &&
                resultsElement[0].TryGetProperty("alternatives", out var alternativesElement) &&
                alternativesElement.GetArrayLength() > 0 &&
                alternativesElement[0].TryGetProperty("transcript", out var transcriptElement))
            {
                transcript = transcriptElement.GetString();
            }
            else
            {
                _logger.LogWarning("Google Speech API không trả về kết quả transcript.");
                return BadRequest("❌ Không thể trích xuất transcript từ Google Speech API.");
            }

            // Step 3: Send to OpenAI for evaluation
            string evalPrompt = $@"
Evaluate the user's response for the following:
Prompt: '{originalPrompt}'
Transcript: '{transcript}'
Give:
- Pronunciation feedback
- Grammar & fluency feedback
- A score out of 100

Format JSON:
{{ ""score"": 90, ""feedback"": ""Good..."" }}
";

            var evaluation = await _aiService.GetAIResponseAsync(evalPrompt, false, new(), "System");

            JsonDocument evalJson;
            try
            {
                evalJson = JsonDocument.Parse(evaluation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi phân tích phản hồi từ OpenAI: {ex.Message}");
                return StatusCode(500, "❌ OpenAI trả phản hồi không hợp lệ.");
            }

            return Ok(new
            {
                prompt = originalPrompt,
                transcript = transcript,
                score = evalJson.RootElement.GetProperty("score").GetInt32(),
                feedback = evalJson.RootElement.GetProperty("feedback").GetString()
            });
        }

    }
}
