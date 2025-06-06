using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class AchievementService : IAchievementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Achievement> _repo;

        public AchievementService(IUnitOfWork uow)
        {
            _uow = uow;
            _repo = _uow.GetRepository<Achievement>();
        }

        public async Task<IEnumerable<Achievement>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Achievement?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<Achievement> AddAsync(Achievement achievement)
        {
            await _repo.AddAsync(achievement);
            await _uow.SaveAsync();
            return achievement;
        }

        public async Task<Achievement> UpdateAsync(Achievement achievement)
        {
            _repo.Update(achievement);
            await _uow.SaveAsync();
            return achievement;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var ach = await _repo.GetByIdAsync(id);
            if (ach == null) return false;
            _repo.Delete(ach);
            await _uow.SaveAsync();
            return true;
        }
    }

}
