using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.Models.Request;

namespace LearnEase.Repository.IRepo
{
    public interface ILeaderboardRepository
    {
        Task<List<Leaderboard>> GetTopUsersAsync(string period, int count);
        Task AddAsync(Leaderboard leaderboard);
        Task<Leaderboard?> GetByIdAsync(Guid id);
        Task<IEnumerable<Leaderboard>> GetAllAsync();
        void Update(Leaderboard leaderboard);
        void Delete(Leaderboard leaderboard);
        Task RecordScoreAsync(RecordScoreDto dto);
    }
}
