using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Service
{
    public class UserStreakService : IUserStreakService
    {
        private readonly LearnEaseContext _context;

        public UserStreakService(LearnEaseContext context)
        {
            _context = context;
        }

        public async Task UpdateStreakAsync(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            var streak = await _context.UserStreaks
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Date == today);

            if (streak == null)
            {
                var yesterday = today.AddDays(-1);
                var yesterdayStreak = await _context.UserStreaks
                    .Where(s => s.UserId == userId && s.Date == yesterday)
                    .FirstOrDefaultAsync();

                int streakCount = 1;
                if (yesterdayStreak != null)
                {
                    var recentDays = await _context.UserStreaks
                        .Where(s => s.UserId == userId && s.Date <= yesterday)
                        .OrderByDescending(s => s.Date)
                        .Take(10)
                        .ToListAsync();

                    streakCount = 1;
                    for (int i = 0; i < recentDays.Count - 1; i++)
                    {
                        if ((recentDays[i].Date - recentDays[i + 1].Date).TotalDays == 1)
                            streakCount++;
                        else
                            break;
                    }
                    streakCount += 1;
                }

                _context.UserStreaks.Add(new UserStreak
                {
                    UserId = userId,
                    Date = today,
                    LessonCount = 1
                });

                await _context.SaveChangesAsync();

                if (new[] { 5, 10, 30 }.Contains(streakCount))
                {
                    _context.Achievements.Add(new Achievement
                    {
                        AchievementId = Guid.NewGuid(),
                        UserId = userId,
                        Name = $"🔥 {streakCount}-Day Streak!",
                        Description = "Keep up the great work!"
                    });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                streak.LessonCount += 1;
                _context.UserStreaks.Update(streak);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCurrentStreakAsync(Guid userId)
        {
            var latest = await _context.UserStreaks
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Date)
                .Take(30)
                .ToListAsync();

            int streak = 0;
            DateTime? prev = null;
            foreach (var s in latest)
            {
                if (prev == null || (prev.Value - s.Date).TotalDays == 1)
                {
                    streak++;
                    prev = s.Date;
                }
                else
                {
                    break;
                }
            }

            return streak;
        }

        public async Task<DateTime?> GetLastActiveDateAsync(Guid userId)
        {
            return await _context.UserStreaks
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.Date)
                .Select(s => (DateTime?)s.Date)
                .FirstOrDefaultAsync();
        }
    }
}


