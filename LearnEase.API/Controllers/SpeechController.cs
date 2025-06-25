using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : Controller
    {
        private const string GoogleApiKey = ""; 
        private const string GoogleApiUrl = $"https://speech.googleapis.com/v1/speech:recognize?key={GoogleApiKey}";

        [HttpPost("transcribe")]
        public async Task<IActionResult> TranscribeAudio(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("File không hợp lệ.");

            byte[] audioBytes;
            using (var memoryStream = new MemoryStream())
            {
                await audioFile.CopyToAsync(memoryStream);
                audioBytes = memoryStream.ToArray();
            }

            var base64Audio = Convert.ToBase64String(audioBytes);

            var requestPayload = new
            {
                config = new
                {
                    encoding = "MP3", // Hoặc "MP3" nếu bạn dùng mp3
                    sampleRateHertz = 16000,
                    languageCode = "en-GB"
                },
                audio = new
                {
                    content = base64Audio
                }
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(GoogleApiUrl, content);
            var json = await response.Content.ReadAsStringAsync();

            return Ok(JsonDocument.Parse(json));
        }
    }

}
