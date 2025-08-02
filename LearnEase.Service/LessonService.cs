using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Response;

namespace LearnEase.Service
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Lesson> _lessonRepo;

        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _lessonRepo = _unitOfWork.GetRepository<Lesson>();
        }

        public async Task<List<LessonResponse>> GetLessonsByTopicAsync(Guid topicId)
        {
            var lessons = await _lessonRepo.GetAllAsync();
            var filtered = lessons.Where(l => l.TopicId == topicId);

            return filtered.Select(l => new LessonResponse
            {
                LessonId = l.LessonId,
                TopicId = l.TopicId,
                Title = l.Title
            }).ToList();
        }
    }
}
