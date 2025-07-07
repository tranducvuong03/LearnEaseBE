using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.IServices
{
    public interface IAiLessonService
    {
        Task<AiLesson> GenerateLessonAsync(string topic);
        Task<AiLessonPart> GenerateWritingPart(string topic);
        Task<AiLessonPart> GenerateReadingPart(string topic);
        Task<AiLessonPart> GenerateListeningPart(string topic);
        Task<AiLessonPart> GenerateSpeakingPart(string topic);
        Task<string> TranslateTopicToEnglish(string topic);
        Task<AiLesson?> GetCurrentWeeklyLessonAsync();
        Task<AiLesson> GetOrGenerateCurrentWeeklyLessonAsync();
        Task<List<AiLesson>> GetOrGenerateWeeklyLessonsAsync();
    }
}
