using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface IAchievementService
    {
        Task<IEnumerable<Achievement>> GetAllAsync();
        Task<Achievement?> GetByIdAsync(Guid id);
        Task<Achievement> AddAsync(Achievement achievement);
        Task<Achievement> UpdateAsync(Achievement achievement);
        Task<bool> DeleteAsync(Guid id);
    }

}
