﻿using LearnEase.Repository.EntityModel;
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
        var prompt = $"Generate a short writing prompt for English learners about '{topic}'. Ask them to write 3–5 sentences.";
        var result = await _openAIService.GetAIResponseAsync(prompt, false, new(), "System");

        return new AiLessonPart
        {
            PartId = Guid.NewGuid(),
            Skill = SkillType.Writing,
            Prompt = result.Trim(),
            ReferenceText = null,
            AudioUrl = null,
            ChoicesJson = null
        };
    }

    public async Task<AiLessonPart> GenerateReadingPart(string topic)
    {
        var prompt = $@"
Write a short English paragraph (~50 words) about '{topic}'.
Then generate 3 multiple choice questions with 4 options (A–D) each.
Include correct answers.
Return in JSON format:
{{ ""passage"": ""..."", ""questions"": [{{ ""question"": ""..."", ""choices"": [...], ""answer"": ""A"" }}] }}
";

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
        var prompt = $"Generate one simple English sentence (max 10 words) for listening practice about '{topic}'. Return only the sentence.";
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
        var prompt = $"Generate a short speaking prompt (max 15 words) for English learners about '{topic}'. Return only the sentence.";
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
