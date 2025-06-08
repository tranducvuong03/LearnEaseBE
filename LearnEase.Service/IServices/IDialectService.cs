using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface IDialectService
    {
        Task<IEnumerable<Dialect>> GetAllAsync();
        Task<Dialect?> GetByIdAsync(Guid id);
        Task<Dialect> AddAsync(Dialect dialect);
        Task<Dialect> UpdateAsync(Dialect dialect);
        Task<bool> DeleteAsync(Guid id);
    }

}
