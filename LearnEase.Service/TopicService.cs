using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Topic> _repo;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Topic>();
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
    }
}
