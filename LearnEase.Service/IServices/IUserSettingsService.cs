using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.IServices
{
    public interface IUserSettingsService
    {
        Task<IEnumerable<UserSettings>> GetAllAsync();
        Task<UserSettings?> GetByIdAsync(Guid id);
        Task<UserSettings> AddAsync(UserSettings settings);
        Task<UserSettings> UpdateAsync(UserSettings settings);
        Task<bool> DeleteAsync(Guid id);
    }

}
