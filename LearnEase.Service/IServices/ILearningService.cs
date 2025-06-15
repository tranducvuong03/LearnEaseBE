using LearnEase.Service.Models.Response;

namespace LearnEase.Service.IServices
{
    public interface ILearningService
    {
        // Phương thức này sẽ trả về bài học tiếp theo cho một người dùng cụ thể
        Task<NextLesson?> GetNextLessonForUserAsync(Guid userId);
        // Bạn có thể thêm các phương thức khác liên quan đến logic học tập tại đây
        // Ví dụ: Task UpdateUserProgressAsync(Guid userId, Guid lessonId, string lessonType, int score);
    }
}
