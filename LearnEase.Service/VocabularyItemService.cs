using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class VocabularyItemService : IVocabularyItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<VocabularyItem> _repo;

        public VocabularyItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<VocabularyItem>();
        }

        public async Task<IEnumerable<VocabularyItem>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<VocabularyItem?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<VocabularyItem> AddAsync(VocabularyItem item)
        {
            await _repo.AddAsync(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<VocabularyItem> UpdateAsync(VocabularyItem item)
        {
            _repo.Update(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }

}
