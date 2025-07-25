using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Topic> _repo;
        private readonly IGenericRepository<UserTopicProgress> _progressRepo;
        private readonly IGenericRepository<UserProgress> _userProgressRepo;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Topic>();
            _progressRepo = _unitOfWork.GetRepository<UserTopicProgress>();
            _userProgressRepo = _unitOfWork.GetRepository<UserProgress>();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Topic?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Topic> AddAsync(Topic topic)
        {
            topic.TopicId = Guid.NewGuid();
            await _repo.AddAsync(topic);
            await _unitOfWork.SaveAsync();
            return topic;
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            _repo.Update(topic);
            await _unitOfWork.SaveAsync();
            return topic;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var topic = await _repo.GetByIdAsync(id);
            if (topic == null) return false;

            _repo.Delete(topic);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Topic>> GetWithLessonsAsync()
        {
            return await _repo.GetAllAsync(
                include: q => q.Include(t => t.Lessons)
            );
        }

        public async Task<IEnumerable<TopicProgressResponse>> GetTopicsWithProgress(Guid userId)
        {
            var topics = await _repo.GetAllAsync(
                include: q => q.Include(t => t.Lessons)
            );

            var progresses = await _progressRepo.GetAllAsync();

            var userProgressDict = progresses
                .Where(p => p.UserId == userId)
                .ToDictionary(p => p.TopicId, p => p.CompletedLessonCount);

            var result = topics.Select(topic => new TopicProgressResponse
            {
                TopicId = topic.TopicId,
                Title = topic.Title,
                Description = topic.Description,
                TotalLessons = topic.Lessons?.Count ?? 0,
                CompletedLessons = userProgressDict.ContainsKey(topic.TopicId)
                    ? userProgressDict[topic.TopicId]
                    : 0
            });

            return result;
        }

        public async Task UpdateCompletedLessonCountAsync(Guid userId, Guid topicId)
        {
            // 1. Lấy tất cả Lesson thuộc topic này
            var lessonRepo = _unitOfWork.GetRepository<Lesson>();
            var lessons = await lessonRepo.GetAllAsync(
                include: null,
                predicate: l => l.TopicId == topicId
            );
            var lessonIds = lessons.Select(l => l.LessonId).ToList();

            // 2. Lấy tất cả UserProgress đúng của user cho các lesson đó
            var correctProgresses = await _userProgressRepo.GetAllAsync(
                predicate: p =>
                    p.UserId == userId &&
                    lessonIds.Contains(p.LessonId) &&
                    p.IsCorrect
            );

            // 3. Nhóm theo LessonId và đếm xem mỗi nhóm có >= 8 câu đúng không
            var completedLessonCount = correctProgresses
                .GroupBy(p => p.LessonId)
                .Count(g => g.Count() >= 8);

            // 4. Lấy hoặc tạo mới record UserTopicProgress
            var existing = (await _progressRepo.GetAllAsync(
                predicate: p =>
                    p.UserId == userId &&
                    p.TopicId == topicId
            )).FirstOrDefault();

            if (existing == null)
            {
                await _progressRepo.AddAsync(new UserTopicProgress
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TopicId = topicId,
                    CompletedLessonCount = completedLessonCount
                });
            }
            else
            {
                existing.CompletedLessonCount = completedLessonCount;
                _progressRepo.Update(existing);
            }

            // 5. Lưu vào database
            await _unitOfWork.SaveAsync();
        }
    }
}
