using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Repository.IRepo;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepository _leader;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Leaderboard> _repo;

        public LeaderboardService(IUnitOfWork uow, ILeaderboardRepository leader)
        {
            _leader = leader;
            _uow = uow;
            _repo = _uow.GetRepository<Leaderboard>();
        }

        public async Task<IEnumerable<Leaderboard>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Leaderboard?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<Leaderboard> AddAsync(Leaderboard leaderboard)
        {
            await _repo.AddAsync(leaderboard);
            await _uow.SaveAsync();
            return leaderboard;
        }

        public async Task<Leaderboard> UpdateAsync(Leaderboard leaderboard)
        {
            _repo.Update(leaderboard);
            await _uow.SaveAsync();
            return leaderboard;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var board = await _repo.GetByIdAsync(id);
            if (board == null) return false;
            _repo.Delete(board);
            await _uow.SaveAsync();
            return true;
        }
        public async Task<List<Leaderboard>> GetTopUsersAsync(string period, int count)
        {
            return await _leader.GetTopUsersAsync(period, count);
        }
        public async Task RecordScoreAsync(RecordScoreRequest request)

        {
            await _leader.RecordScoreAsync(request);
        }

    }

}
