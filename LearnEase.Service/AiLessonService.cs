using LearnEase.Repository.EntityModel;
using LearnEase.Repository;
using LearnEase.Service.IServices;
using System.Text.Json;
using LearnEase.Repository.Base;
using LearnEase.Service;

public class AiLessonService : IAiLessonService
{
    private readonly IOpenAIService _openAIService;
    private readonly IGenericRepository<AiLesson> _lessonRepo;
    private readonly IGenericRepository<AiLessonPart> _partRepo;
    private readonly IUnitOfWork _uow;

    public AiLessonService(IOpenAIService openAIService, IUnitOfWork uow)
    {
        _openAIService = openAIService;
        _uow = uow;
        _lessonRepo = _uow.GetRepository<AiLesson>();
        _partRepo = _uow.GetRepository<AiLessonPart>();
    }

    public async Task<AiLessonPart> GenerateWritingPart(string topic)
    {
        var prompt = $@"
Create a simple writing prompt for English learners (A2–B1 level) about the topic: '{topic}'.
Use less than 20 words.
Make it clear and easy to understand.
Learners will write 3–5 sentences.
Return only the prompt sentence.";

        var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Writing,
            Prompt = result.Trim('\"', '.', '\n'),
            ReferenceText = null,
            AudioUrl = null,
            ChoicesJson = null
        };
    }


    public async Task<AiLessonPart> GenerateReadingPart(string topic)
    {
        var prompt = $@"
Write a short English paragraph (~40–50 words) about '{topic}'.
Then generate 3 multiple choice questions with 4 options (A–D).
Each question should be simple and suitable for A2–B1 learners.
Include correct answers.
Return in JSON format:
{{ ""passage"": ""..."", ""questions"": [{{ ""question"": ""..."", ""choices"": [...], ""answer"": ""A"" }}] }}";


        var json = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
        var parsed = JsonDocument.Parse(json);
        var passage = parsed.RootElement.GetProperty("passage").GetString();
        var questions = parsed.RootElement.GetProperty("questions").ToString();

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Reading,
            Prompt = "Read the passage and answer the questions.",
            ReferenceText = passage?.Trim(),
            AudioUrl = null,
            ChoicesJson = questions
        };
    }

    public async Task<AiLessonPart> GenerateListeningPart(string topic)
    {
        var prompt = $@"
Create one simple English sentence (max 10 words) for listening practice about '{topic}'.
Use everyday vocabulary and A2–B1 grammar.
Return only the sentence.";
        var sentence = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
        string cleaned = sentence.Trim().Trim('"', '.', '\n');

        // 1. Tạo file mp3 từ câu
        var audioGen = new AudioGenerator();
        var audioBytes = await audioGen.GenerateMp3Async(cleaned);

        // 2. Upload lên Google Cloud Storage
        var uploader = new GcsUploader();
        string fileName = $"{Guid.NewGuid()}.mp3";
        string audioUrl = await uploader.UploadMp3Async(audioBytes, fileName);

        // 3. Trả về AiLessonPart đã có URL thật
        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Listening,
            Prompt = "Listen to the audio and type exactly what you hear.",
            ReferenceText = cleaned,
            AudioUrl = audioUrl,
            ChoicesJson = null
        };
    }


    public async Task<AiLessonPart> GenerateSpeakingPart(string topic)
    {
        var prompt = $@"
Create a short speaking prompt (max 12 words) for English learners (A2–B1 level) about '{topic}'.
Use simple vocabulary.
Return only the prompt sentence.";
        var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Speaking,
            Prompt = result.Trim(),
            ReferenceText = null,
            AudioUrl = null,
            ChoicesJson = null
        };
    }
    public async Task<List<AiLesson>> GetOrGenerateWeeklyLessonsAsync()
    {
        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        var existing = (await _lessonRepo.GetAllAsync())
                        .Where(l => l.CreatedAt >= startOfWeek)
                        .OrderBy(l => l.DayIndex)
                        .ToList();

        if (existing.Count == 7)
            return existing;

        // 1️⃣ Tạo topic mới
        string topicPrompt = "Generate one simple English learning topic of the week. Return only the topic in a few words.";
        string topic = await _openAIService.GetAIResponseAsync(topicPrompt, false, new(), "System");
        topic = topic.Trim('\"', '.', '\n');

        // 2️⃣ Tạo 7 bài học với độ khó tăng dần
        var allLessons = new List<AiLesson>();
        for (int day = 1; day <= 7; day++)
        {
            var lesson = new AiLesson
            {
                LessonId = Guid.NewGuid(),
                Topic = topic,
                CreatedAt = DateTime.UtcNow,
                DayIndex = day
            };
            await _lessonRepo.AddAsync(lesson);

            var parts = new List<AiLessonPart>
        {
            await GenerateUniqueSpeakingPart(topic, day),
            await GenerateUniqueListeningPart(topic, day),
            await GenerateUniqueReadingPart(topic, day),
            await GenerateUniqueWritingPart(topic, day)
        };

            foreach (var part in parts)
            {
                part.LessonId = lesson.LessonId;
                await _partRepo.AddAsync(part);
            }

            allLessons.Add(lesson);
        }

        await _uow.SaveAsync();
        return allLessons;
    }
    public async Task<AiLessonPart> GenerateUniqueWritingPart(string topic, int day)
    {
        var existingPrompts = (await _partRepo.GetAllAsync())
            .Where(p => p.Skill == SkillType.Writing && p.Lesson.Topic == topic)
            .Select(p => p.Prompt.Trim().ToLower())
            .ToHashSet();

        string result;
        int retries = 0;

        do
        {
            var prompt = $@"
Create a simple and unique writing prompt (≤ 20 words) about '{topic}'.
This is day {day}/7. Avoid repeating prompts used in previous lessons with same topic.
Return only the prompt sentence.";

            result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
            result = result.Trim('\"', '.', '\n').ToLower();
            retries++;
        }
        while (existingPrompts.Contains(result) && retries < 3);

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Writing,
            Prompt = result,
            ReferenceText = null
        };
    }

    public async Task<AiLessonPart> GenerateUniqueListeningPart(string topic, int day)
    {
        var existing = (await _partRepo.GetAllAsync())
            .Where(p => p.Skill == SkillType.Listening && p.Lesson.Topic == topic)
            .Select(p => p.ReferenceText?.Trim().ToLower())
            .ToHashSet();

        string sentence = "";
        int retries = 0;

        do
        {
            var prompt = $@"
Generate a short (<10 words) English sentence for listening practice.
Topic: '{topic}', Day: {day}/7. A2–B1 level. Avoid repeating previous sentences on this topic.
Return only the sentence.";

            var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
            sentence = result.Trim('\"', '.', '\n').ToLower();
            retries++;

        } while (existing.Contains(sentence) && retries < 3);

        // Generate audio
        var audioGen = new AudioGenerator();
        var audioBytes = await audioGen.GenerateMp3Async(sentence);

        var uploader = new GcsUploader();
        var fileName = $"{Guid.NewGuid()}.mp3";
        var audioUrl = await uploader.UploadMp3Async(audioBytes, fileName);

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Listening,
            Prompt = "Listen to the audio and type exactly what you hear.",
            ReferenceText = sentence,
            AudioUrl = audioUrl
        };
    }
    public async Task<AiLessonPart> GenerateUniqueSpeakingPart(string topic, int day)
    {
        var existing = (await _partRepo.GetAllAsync())
            .Where(p => p.Skill == SkillType.Speaking && p.Lesson.Topic == topic)
            .Select(p => p.Prompt.Trim().ToLower())
            .ToHashSet();

        string resultText = "";
        int retries = 0;

        do
        {
            var prompt = $@"
Create a short speaking prompt (max 12 words) for English learners (A2–B1) about '{topic}'.
This is Day {day}/7. Avoid repeating any previous prompt on same topic.
Return only the prompt sentence.";

            var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
            resultText = result.Trim('\"', '.', '\n').ToLower();
            retries++;

        } while (existing.Contains(resultText) && retries < 3);

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Speaking,
            Prompt = resultText
        };
    }

    public async Task<AiLessonPart> GenerateUniqueReadingPart(string topic, int day)
    {
        var existingPassages = (await _partRepo.GetAllAsync())
            .Where(p => p.Skill == SkillType.Reading && p.Lesson.Topic == topic)
            .Select(p => p.ReferenceText?.Trim().ToLower())
            .ToHashSet();

        string passage = "", questionsJson = "";
        int retries = 0;

        do
        {
            var prompt = $@"
Write a short English paragraph (40–50 words) about '{topic}' for A2–B1 learners.
Then create 3 multiple choice questions (A–D) based on it.
Make it unique for Day {day}/7. Avoid repeating previous paragraphs on same topic.
Return as:
{{ ""passage"": ""..."", ""questions"": [{{ ""question"": ""..."", ""choices"": [...], ""answer"": ""A"" }}] }}";

            var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");

            var parsed = JsonDocument.Parse(result);
            passage = parsed.RootElement.GetProperty("passage").GetString()?.Trim().ToLower() ?? "";
            questionsJson = parsed.RootElement.GetProperty("questions").ToString();

            retries++;
        }
        while (existingPassages.Contains(passage) && retries < 3);

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Reading,
            Prompt = "Read the passage and answer the questions.",
            ReferenceText = passage,
            ChoicesJson = questionsJson
        };
    }


    public async Task<AiLesson> GenerateLessonAsync(string topic)
    {
        var translatedTopic = await TranslateTopicToEnglish(topic);
        var lesson = new AiLesson
        {
            LessonId = Guid.NewGuid(),
            Topic = topic,
            CreatedAt = DateTime.UtcNow
        };

        var parts = new List<AiLessonPart>
        {
            await GenerateSpeakingPart(topic),
            await GenerateListeningPart(topic),
            await GenerateReadingPart(topic),
            await GenerateWritingPart(topic)
        };

        await _lessonRepo.AddAsync(lesson);
        foreach (var part in parts)
        {
            part.LessonId = lesson.LessonId;
            await _partRepo.AddAsync(part);
        }

        await _uow.SaveAsync();
        return lesson;
    }
    public async Task<string> TranslateTopicToEnglish(string topic)
    {
        var prompt = $@"Translate this Vietnamese topic into English. Return ONLY the English phrase.
Topic: '{topic}'";

        var translation = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");
        return translation.Trim().Trim('"', '.', '\n');
    }
    public async Task<AiLesson?> GetCurrentWeeklyLessonAsync()
    {
        var startOfWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        return (await _lessonRepo.GetAllAsync())
            .Where(l => l.CreatedAt >= startOfWeek)
            .OrderByDescending(l => l.CreatedAt)
            .FirstOrDefault();
    }

    public async Task<AiLesson> GetOrGenerateCurrentWeeklyLessonAsync()
    {
        var existing = await GetCurrentWeeklyLessonAsync();
        if (existing != null) return existing;

        // Tự động sinh topic nếu chưa có
        var topicPrompt = "Generate one simple English learning topic of the week. Return only the topic in a few words.";
        var topic = await _openAIService.GetAIResponseAsync(topicPrompt, false, new(), "System");

        return await GenerateLessonAsync(topic.Trim('\"', '.', '\n'));
    }


}
