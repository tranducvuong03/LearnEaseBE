using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface IUserProgressService
    {
        Task<IEnumerable<UserProgress>> GetAllAsync();
        Task<UserProgress?> GetByIdAsync(Guid id);
        Task<UserProgress> AddAsync(UserProgress progress);
        Task<UserProgress> UpdateAsync(UserProgress progress);
        Task<bool> DeleteAsync(Guid id);
    }

}
