using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;

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
