using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Language> _repo;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Language>();
        }

        public async Task<IEnumerable<Language>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Language?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<Language> AddAsync(Language language)
        {
            await _repo.AddAsync(language);
            await _unitOfWork.SaveAsync();
            return language;
        }

        public async Task<Language> UpdateAsync(Language language)
        {
            _repo.Update(language);
            await _unitOfWork.SaveAsync();
            return language;
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
