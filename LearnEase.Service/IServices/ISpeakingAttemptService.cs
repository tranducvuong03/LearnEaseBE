using LearnEase.Repository.EntityModel;

namespace LearnEase.Service
{
    public interface ISpeakingAttemptService
    {
        Task<IEnumerable<SpeakingAttempt>> GetAllAsync();
        Task<SpeakingAttempt?> GetByIdAsync(Guid id);
        Task<SpeakingAttempt> AddAsync(SpeakingAttempt attempt);
        Task<SpeakingAttempt> UpdateAsync(SpeakingAttempt attempt);
        Task<bool> DeleteAsync(Guid id);
    }

}
