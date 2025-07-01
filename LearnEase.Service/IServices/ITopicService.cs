using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.IServices
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic?> GetByIdAsync(Guid id);
        Task<Topic> AddAsync(Topic topic);
        Task<Topic> UpdateAsync(Topic topic);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Topic>> GetWithLessonsAsync();
    }
}
