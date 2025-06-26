using LearnEase.Repository.EntityModel;
using LearnEase.Repository.IRepo;
using LearnEase.Service.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly LearnEaseContext _context;

        public LeaderboardRepository(LearnEaseContext context)
        {
            _context = context;
        }

        public async Task<List<Leaderboard>> GetTopUsersAsync(string period, int count)
        {
            return await _context.Leaderboards
                .Where(l => l.Period == period)
                .OrderByDescending(l => l.Score)
                .Include(l => l.User)
                .Take(count)
                .ToListAsync();
        }

        public async Task RecordScoreAsync(RecordScoreRequest request)
        {
            var existing = await _context.Leaderboards
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Period == request.Period);

            if (existing != null)
            {
                existing.Score = request.Score;
                existing.RecordedAt = DateTime.UtcNow;
                _context.Leaderboards.Update(existing);
            }
            else
            {
                var newScore = new Leaderboard
                {
                    LeaderboardId = Guid.NewGuid(),
                    UserId = request.UserId,
                    Period = request.Period,
                    Score = request.Score,
                    RecordedAt = DateTime.UtcNow
                };

                await _context.Leaderboards.AddAsync(newScore);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Leaderboard leaderboard)
        {
            await _context.Leaderboards.AddAsync(leaderboard);
        }

        public async Task<Leaderboard?> GetByIdAsync(Guid id)
        {
            return await _context.Leaderboards
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.LeaderboardId == id);
        }

        public async Task<IEnumerable<Leaderboard>> GetAllAsync()
        {
            return await _context.Leaderboards.Include(l => l.User).ToListAsync();
        }

        public void Update(Leaderboard leaderboard)
        {
            _context.Leaderboards.Update(leaderboard);
        }

        public void Delete(Leaderboard leaderboard)
        {
            _context.Leaderboards.Remove(leaderboard);
        }
    }
}
