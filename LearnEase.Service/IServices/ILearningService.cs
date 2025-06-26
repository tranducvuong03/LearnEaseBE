using LearnEase.Service.Models.Response;

namespace LearnEase.Service.IServices
{
    public interface ILearningService
    {
        Task<NextLesson?> GetNextLessonForUserAsync(Guid userId);
        Task<LessonModel?> GetNextLessonBlockForUserAsync(Guid userId);

    }
}
