using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface IVocabularyItemService
    {
        Task<IEnumerable<VocabularyItem>> GetAllAsync();
        Task<VocabularyItem?> GetByIdAsync(Guid id);
        Task<VocabularyItem> AddAsync(VocabularyItem item);
        Task<VocabularyItem> UpdateAsync(VocabularyItem item);
        Task<bool> DeleteAsync(Guid id);
    }

}
