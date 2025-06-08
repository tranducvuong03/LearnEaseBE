using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Leaderboard> _repo;

        public LeaderboardService(IUnitOfWork uow)
        {
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
    }

}
