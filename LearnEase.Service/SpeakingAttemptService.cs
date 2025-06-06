using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class SpeakingAttemptService : ISpeakingAttemptService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<SpeakingAttempt> _repo;

        public SpeakingAttemptService(IUnitOfWork uow)
        {
            _uow = uow;
            _repo = _uow.GetRepository<SpeakingAttempt>();
        }

        public async Task<IEnumerable<SpeakingAttempt>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<SpeakingAttempt?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<SpeakingAttempt> AddAsync(SpeakingAttempt attempt)
        {
            await _repo.AddAsync(attempt);
            await _uow.SaveAsync();
            return attempt;
        }

        public async Task<SpeakingAttempt> UpdateAsync(SpeakingAttempt attempt)
        {
            _repo.Update(attempt);
            await _uow.SaveAsync();
            return attempt;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var attempt = await _repo.GetByIdAsync(id);
            if (attempt == null) return false;
            _repo.Delete(attempt);
            await _uow.SaveAsync();
            return true;
        }
    }

}
