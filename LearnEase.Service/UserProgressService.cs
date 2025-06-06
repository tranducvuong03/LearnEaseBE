using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class UserProgressService : IUserProgressService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<UserProgress> _repo;

        public UserProgressService(IUnitOfWork uow)
        {
            _uow = uow;
            _repo = _uow.GetRepository<UserProgress>();
        }

        public async Task<IEnumerable<UserProgress>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<UserProgress?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<UserProgress> AddAsync(UserProgress progress)
        {
            await _repo.AddAsync(progress);
            await _uow.SaveAsync();
            return progress;
        }

        public async Task<UserProgress> UpdateAsync(UserProgress progress)
        {
            _repo.Update(progress);
            await _uow.SaveAsync();
            return progress;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var progress = await _repo.GetByIdAsync(id);
            if (progress == null) return false;
            _repo.Delete(progress);
            await _uow.SaveAsync();
            return true;
        }
    }

}
