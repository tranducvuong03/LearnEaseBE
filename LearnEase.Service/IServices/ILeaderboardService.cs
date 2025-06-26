using LearnEase.Repository.EntityModel;
using LearnEase.Service.Models.Request;

namespace LearnEase.Service
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<Leaderboard>> GetAllAsync();
        Task<Leaderboard?> GetByIdAsync(Guid id);
        Task<Leaderboard> AddAsync(Leaderboard leaderboard);
        Task<Leaderboard> UpdateAsync(Leaderboard leaderboard);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Leaderboard>> GetTopUsersAsync(string period, int count);
        Task RecordScoreAsync(RecordScoreRequest request);
    }

}
