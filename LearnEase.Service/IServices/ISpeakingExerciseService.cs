using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface ISpeakingExerciseService
    {
        Task<IEnumerable<SpeakingExercise>> GetAllAsync();
        Task<SpeakingExercise?> GetByIdAsync(Guid id);
        Task<SpeakingExercise> AddAsync(SpeakingExercise exercise);
        Task<SpeakingExercise> UpdateAsync(SpeakingExercise exercise);
        Task<bool> DeleteAsync(Guid id);
    }

}
