using System.Text;
using System.Text.RegularExpressions;
using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LearnEase.Service.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly LearnEaseContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly ILogger<OpenAIService> _logger;

        public OpenAIService(LearnEaseContext context, HttpClient httpClient, IConfiguration configuration, ILogger<OpenAIService> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _apiUrl = "https://api.openai.com/v1/chat/completions";
            _apiKey = configuration["OpenAI:ApiKey"];
            _logger = logger;
        }
        public async Task<string> GenerateQuizFeedbackAsync(int correct, int total)
        {
            string prompt = $@"
A learner just completed a quiz.

Correct answers: {correct} out of {total}

Write a short feedback in English, friendly tone, max 2 sentences.
";

            return await CallOpenAI(prompt);
        }

        public async Task<string> GetAIResponseAsync(string userInput, bool useDatabase = false, List<object> conversationHistory = null, string username = "bạn")
        {
            _logger.LogInformation($"Received input: {userInput}, useDatabase: {useDatabase}");

            if (!useDatabase && (conversationHistory == null || !conversationHistory.Any()))
            {
                // Trong trường hợp cần AI sinh ra nội dung trực tiếp (ví dụ: prompt speaking)
                return await CallOpenAI(userInput); // dùng trực tiếp userInput như prompt
            }

            string lowerInput = userInput.ToLower();

            if (lowerInput.Contains("từ vựng") || lowerInput.Contains("vocabulary"))
            {
                return await SuggestVocabularyPractice(userInput);
            }

            if (lowerInput.Contains("đoạn văn") || lowerInput.Contains("paragraph") || lowerInput.Contains("viết đoạn"))
            {
                return await SuggestParagraphPractice(userInput);
            }

            return await HandleConversationWithGrammarCheck(userInput, conversationHistory);
        }

        private async Task<string> HandleConversationWithGrammarCheck(string userInput, List<object> conversationHistory = null)
        {
            string correctedText = await CheckGrammarAndSuggestFix(userInput);

            return correctedText ?? await GenerateGeneralResponse(userInput, conversationHistory);
        }

        private async Task<string> CheckGrammarAndSuggestFix(string userInput)
        {
            string prompt = $@"
Bạn là một giáo viên tiếng Anh thân thiện. 
Hãy kiểm tra các từ và cụm từ trong câu sau để tìm lỗi chính tả hoặc ngữ pháp.
Nếu có lỗi, hãy trả lời theo mẫu:

Lỗi 1: [nội dung lỗi]  
Sửa lại: [gợi ý sửa]

Nếu không có lỗi, trả về: Không có lỗi.

Câu của người dùng: {userInput}";

            string response = await CallOpenAI(prompt);

            if (response.ToLower().Contains("không có lỗi"))
            {
                return null;
            }

            return $"Tôi đã kiểm tra và phát hiện lỗi sau:\n{response}";
        }

        private async Task<string> SuggestVocabularyPractice(string topic)
        {
            string prompt = $"Hãy gợi ý cho tôi 5 từ vựng tiếng Anh về chủ đề: {topic}, kèm nghĩa tiếng Việt và ví dụ.";
            return await CallOpenAI(prompt);
        }

        private async Task<string> SuggestParagraphPractice(string topic)
        {
            string prompt = $"Viết một đoạn văn ngắn tiếng Anh (khoảng 4-5 câu) về chủ đề: {topic}, kèm bản dịch tiếng Việt.";
            return await CallOpenAI(prompt);
        }

        private async Task<string> GenerateGeneralResponse(string userInput, List<object> conversationHistory = null)
        {
            return await CallOpenAI(userInput, conversationHistory);
        }

        private async Task<string> CallOpenAI(string prompt, List<object> conversationHistory = null)
        {
            var messages = new List<object>
            {
                new { role = "system", content = "Bạn là trợ lý ảo của LearnEase, một ứng dụng học trực tuyến chuyên hỗ trợ người học tiếng Anh." }
            };

            if (conversationHistory != null)
            {
                messages.AddRange(conversationHistory);
            }

            messages.Add(new { role = "user", content = prompt });

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = messages
            };

            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"OpenAI API Error: {response.StatusCode} - {error}");
                return $"Lỗi: {response.StatusCode} - {error}";
            }

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseString);

            try
            {
                return responseObject.choices[0].message.content.ToString().Trim();
            }
            catch (Exception ex)
            {
                _logger.LogError($"JSON Parsing Error: {ex.Message} - Raw Response: {responseString}");
                return $"Lỗi phân tích JSON: {ex.Message} - Phản hồi thô: {responseString}";
            }
        }
    }
}