using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using Google.Cloud.TextToSpeech.V1;
using LearnEase.Repository.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Google.Rpc;
using LearnEase.Repository.DTO;
using System.Runtime.InteropServices;

namespace LearnEase.Api.Controllers
{

    [Route("api/speaking-ai")]
    [ApiController]
    public class SpeakingAIController : ControllerBase
    {
        private readonly IOpenAIService _aiService;
        private readonly LearnEaseContext _context;
        private readonly ILogger<SpeakingAIController> _logger;

      
     
        public SpeakingAIController(IOpenAIService aiService, ILogger<SpeakingAIController> logger, LearnEaseContext context)
        {
            _aiService = aiService;
            _logger = logger;
            _context = context;
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

        [HttpPost("Speaking-score-challenge")]
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
        [HttpGet("drills")]
        public async Task<IActionResult> GetSpeakingDrills(
        [FromQuery] Guid dialectId,
        [FromQuery] int count = 50,
        [FromQuery] int level = 1)
        {
            var dialect = await _context.Dialects.FindAsync(dialectId);
            if (dialect == null || string.IsNullOrEmpty(dialect.AccentCode))
                return BadRequest("❌ Dialect không hợp lệ hoặc thiếu AccentCode.");

            // Lấy các câu đã có trong DB
            var existing = await _context.SpeakingExercises
                .Where(e => e.DialectId == dialectId)
                .OrderByDescending(e => e.ExerciseId)
                .Take(count)
                .ToListAsync();

            if (existing.Count == count)
            {
                return Ok(existing.Select(e => new
                {
                    prompt = e.Prompt,
                    audioUrl = e.SampleAudioUrl
                }));
            }

            // Cần thêm từ GPT
            int toGenerate = count - existing.Count;

            var prompt = $@"
You are an English teacher. Generate {toGenerate} simple English sentences (max 8 words)
for speaking practice at Level {level}.

👉 Return ONLY a valid JSON array. For example:
[""Hello there."", ""How are you?"", ""Let's go now!""]

No explanation. No markdown. JSON array only.
";



            var gptResponse = await _aiService.GetAIResponseAsync(prompt, false, new(), "System");
            List<string> newSentences;
            try
            {
                newSentences = JsonSerializer.Deserialize<List<string>>(gptResponse);
            }
            catch
            {
                return StatusCode(500, "❌ GPT trả về không đúng định dạng JSON array.");
            }

            var ttsClient = TextToSpeechClient.Create();
            var uploader = new GcsUploader(); // class bạn đã có

            var newExercises = new List<SpeakingExercise>();

            foreach (var text in newSentences)
            {
                var input = new SynthesisInput { Text = text };
                var voice = new VoiceSelectionParams
                {
                    LanguageCode = dialect.AccentCode,
                    SsmlGender = SsmlVoiceGender.Neutral
                };
                var config = new AudioConfig { AudioEncoding = AudioEncoding.Mp3 };

                var ttsResponse = await ttsClient.SynthesizeSpeechAsync(input, voice, config);
                var audioBytes = ttsResponse.AudioContent.ToByteArray();

                if (audioBytes.Length == 0) continue;

                var safeSlug = text.ToLowerInvariant().Replace(" ", "-").Replace(".", "").Replace("?", "");
                var audioUrl = await uploader.UploadMp3Async(audioBytes, $"{safeSlug}.mp3");

                var exercise = new SpeakingExercise
                {
                    ExerciseId = Guid.NewGuid(),
                    DialectId = dialectId,
                    Prompt = text,
                    SampleAudioUrl = audioUrl,
                    ReferenceText = text
                };

                newExercises.Add(exercise);
                _context.SpeakingExercises.Add(exercise);
            }

            await _context.SaveChangesAsync();

            var result = existing.Select(e => new
            {
                prompt = e.Prompt,
                audioUrl = e.SampleAudioUrl
            })
            .Concat(newExercises.Select(e => new
            {
                prompt = e.Prompt,
                audioUrl = e.SampleAudioUrl
            }));

            return Ok(result);
        }
        [HttpPost("evaluate-drill")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> EvaluateDrill([FromForm] EvaluateDrillRequest request)
        {
            if (request.AudioFile == null || request.ExerciseId == Guid.Empty)
                return BadRequest("Thiếu audio hoặc exerciseId.");

            var audioFile = request.AudioFile;
            var exerciseId = request.ExerciseId;

            // Lấy bài luyện nói và accent
            var exercise = await _context.SpeakingExercises
                .Include(e => e.Dialect)
                .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId);

            if (exercise == null || exercise.Dialect == null || string.IsNullOrEmpty(exercise.Dialect.AccentCode))
                return BadRequest("❌ Không tìm thấy bài luyện hoặc accent.");

            var base64Audio = "";
            using (var memoryStream = new MemoryStream())
            {
                await audioFile.CopyToAsync(memoryStream);
                base64Audio = Convert.ToBase64String(memoryStream.ToArray());
            }

            var googleRequest = new
            {
                config = new
                {
                    encoding = "MP3",
                    sampleRateHertz = 16000,
                    languageCode = exercise.Dialect.AccentCode
                },
                audio = new
                {
                    content = base64Audio
                }
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(googleRequest), Encoding.UTF8, "application/json");
            var googleResponse = await httpClient.PostAsync(GoogleApiUrl, content);
            var json = await googleResponse.Content.ReadAsStringAsync();

            JsonDocument parsed;
            try { parsed = JsonDocument.Parse(json); }
            catch { return StatusCode(500, "❌ Không thể phân tích phản hồi từ Google."); }

            string transcript = "";
            if (parsed.RootElement.TryGetProperty("results", out var results) &&
                results.GetArrayLength() > 0 &&
                results[0].TryGetProperty("alternatives", out var alternatives) &&
                alternatives.GetArrayLength() > 0 &&
                alternatives[0].TryGetProperty("transcript", out var transcriptElement))
            {
                transcript = transcriptElement.GetString();
            }
            else
            {
                return BadRequest("❌ Không thể trích xuất transcript từ Google.");
            }

            // Đánh giá bằng GPT
            var evalPrompt = $@"
Evaluate the user's response for the following:
Prompt: '{exercise.Prompt}'
Transcript: '{transcript}'
Give:
- Pronunciation feedback
- Grammar & fluency feedback
- A score out of 100

Return JSON:
{{ ""score"": 90, ""feedback"": ""..."" }}
";
            var gptResult = await _aiService.GetAIResponseAsync(evalPrompt, false, new(), "System");

            JsonDocument evalJson;
            try { evalJson = JsonDocument.Parse(gptResult); }
            catch { return StatusCode(500, "❌ GPT trả về không hợp lệ."); }

            int score = evalJson.RootElement.GetProperty("score").GetInt32();
            string feedback = evalJson.RootElement.GetProperty("feedback").GetString();

            // (Tuỳ chọn) Lưu kết quả nếu cần
            var userId = GetUserIdFromToken(); // viết hàm riêng nếu chưa có
            _context.SpeakingAttempts.Add(new SpeakingAttempt
            {
                AttemptId = Guid.NewGuid(),
                UserId = userId,
                ExerciseId = exerciseId,
                Score = score,
                Transcription = transcript
            });
            await _context.SaveChangesAsync();

            return Ok(new
            {
                prompt = exercise.Prompt,
                transcript,
                score,
                feedback
            });
        }

        private Guid GetUserIdFromToken()
        {
            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (claim == null || !Guid.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException("User ID không hợp lệ hoặc chưa đăng nhập.");

            return userId;
        }
        [HttpPost("accent-score")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EvaluateAccent([FromForm] EvaluateAccentRequest request)
        {
            if (request.AudioFile == null || request.DialectId == Guid.Empty)
                return BadRequest("Thiếu audio hoặc dialectId.");

            var dialect = await _context.Dialects.FindAsync(request.DialectId);
            if (dialect == null || string.IsNullOrEmpty(dialect.AccentCode))
                return BadRequest("Dialect không hợp lệ hoặc không có AccentCode.");

            // Convert audio to base64
            string base64Audio;
            using (var memoryStream = new MemoryStream())
            {
                await request.AudioFile.CopyToAsync(memoryStream);
                base64Audio = Convert.ToBase64String(memoryStream.ToArray());
            }

            var googleRequest = new
            {
                config = new
                {
                    encoding = "MP3",
                    sampleRateHertz = 16000,
                    languageCode = dialect.AccentCode
                },
                audio = new
                {
                    content = base64Audio
                }
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(googleRequest), Encoding.UTF8, "application/json");
            var googleResponse = await httpClient.PostAsync(GoogleApiUrl, content);
            var json = await googleResponse.Content.ReadAsStringAsync();

            JsonDocument parsed;
            try { parsed = JsonDocument.Parse(json); }
            catch { return StatusCode(500, "Lỗi phân tích Google Speech."); }

            string transcript = "";
            if (parsed.RootElement.TryGetProperty("results", out var results) &&
                results.GetArrayLength() > 0 &&
                results[0].TryGetProperty("alternatives", out var alternatives) &&
                alternatives.GetArrayLength() > 0 &&
                alternatives[0].TryGetProperty("transcript", out var transcriptElement))
            {
                transcript = transcriptElement.GetString();
            }
            else
            {
                return BadRequest("Google không trả về transcript.");
            }

            // Prompt châm accent
            var evalPrompt = string.IsNullOrWhiteSpace(request.TargetText)
      ? $@"
Your task is to evaluate the user's accent.
Target accent region: {dialect.Region}

Transcript: '{transcript}'

Give:
- Accent match score (how closely the accent resembles native {dialect.Region})
- Short feedback

Return JSON:
{{ ""score"": 85, ""feedback"": ""...""
}}"
      : $@"
Evaluate the user's pronunciation and accent match.

Expected text: '{request.TargetText}'
Transcript from user: '{transcript}'
Target accent: {dialect.Region}

Give:
- How close the user's accent sounds to native {dialect.Region} speakers
- Mention if there are mispronunciations
- Score from 0 to 100
- Short feedback

Return JSON:
{{ ""score"": 85, ""feedback"": ""...""
}}";

            

            var gptResponse = await _aiService.GetAIResponseAsync(evalPrompt, false, new(), "System");

            JsonDocument evalJson;
            try { evalJson = JsonDocument.Parse(gptResponse); }
            catch { return StatusCode(500, "GPT trả về JSON không hợp lệ."); }

            var score = evalJson.RootElement.GetProperty("score").GetInt32();
            var feedback = evalJson.RootElement.GetProperty("feedback").GetString();

            return Ok(new
            {
                dialect = dialect.Name,
                transcript,
                score,
                feedback
            });
        }
    }
}
