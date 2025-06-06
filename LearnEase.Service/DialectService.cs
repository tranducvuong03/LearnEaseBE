using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class DialectService : IDialectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Dialect> _repo;

        public DialectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Dialect>();
        }

        public async Task<IEnumerable<Dialect>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Dialect?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<Dialect> AddAsync(Dialect dialect)
        {
            await _repo.AddAsync(dialect);
            await _unitOfWork.SaveAsync();
            return dialect;
        }

        public async Task<Dialect> UpdateAsync(Dialect dialect)
        {
            _repo.Update(dialect);
            await _unitOfWork.SaveAsync();
            return dialect;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dialect = await _repo.GetByIdAsync(id);
            if (dialect == null) return false;
            _repo.Delete(dialect);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }

}
