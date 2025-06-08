using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(Guid id);
        Task<Language> AddAsync(Language language);
        Task<Language> UpdateAsync(Language language);
        Task<bool> DeleteAsync(Guid id);
    }

}
