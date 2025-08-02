using LearnEase.Service.IServices;

namespace LearnEase.API.Controllers
{
    public class backgroundJob
    {
        private readonly IAiLessonService _lessonService;

        public backgroundJob(IAiLessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task GenerateWeeklyLessons()
        {
            Console.WriteLine($"⏱️ [Hangfire] Tạo bài học tuần mới - {DateTime.Now}");
            await _lessonService.GetOrGenerateWeeklyLessonsAsync();
        }
    }
}
