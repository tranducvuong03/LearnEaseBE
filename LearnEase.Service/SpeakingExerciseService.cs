using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class SpeakingExerciseService : ISpeakingExerciseService
    {
        private readonly IGenericRepository<SpeakingExercise> _repo;
        private readonly IUnitOfWork _uow;

        public SpeakingExerciseService(IUnitOfWork uow)
        {
            _uow = uow;
            _repo = _uow.GetRepository<SpeakingExercise>();
        }

        public async Task<IEnumerable<SpeakingExercise>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<SpeakingExercise?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);
        public async Task<SpeakingExercise> AddAsync(SpeakingExercise exercise)
        {
            await _repo.AddAsync(exercise);
            await _uow.SaveAsync();
            return exercise;
        }
        public async Task<SpeakingExercise> UpdateAsync(SpeakingExercise exercise)
        {
            _repo.Update(exercise);
            await _uow.SaveAsync();
            return exercise;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            _repo.Delete(e);
            await _uow.SaveAsync();
            return true;
        }
    }

}
