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
        private static TimeZoneInfo GetTzOrDefault(TimeZoneInfo tz)
    => tz ?? TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private static DateTime TodayInTz(TimeZoneInfo tz)
    => TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz).Date;
        public async Task UpdateStreakAsync(Guid userId, TimeZoneInfo tz = null)
        {
            var zone = GetTzOrDefault(tz);
            var today = TodayInTz(zone);

            var streakToday = await _context.UserStreaks
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Date == today);

            if (streakToday == null)
            {
                var yesterday = today.AddDays(-1);
                var hadYesterday = await _context.UserStreaks
                    .AnyAsync(s => s.UserId == userId && s.Date == yesterday);

                int streakCount = 0;
                if (hadYesterday)
                {
                    var recentDays = await _context.UserStreaks
                        .Where(s => s.UserId == userId && s.Date <= yesterday)
                        .OrderByDescending(s => s.Date)
                        .Take(40) // đủ cho mốc 30
                        .ToListAsync();

                    streakCount = 1; // bắt đầu từ yesterday
                    for (int i = 0; i < recentDays.Count - 1; i++)
                    {
                        if ((recentDays[i].Date - recentDays[i + 1].Date).Days == 1)
                            streakCount++;
                        else
                            break;
                    }
                    streakCount += 1; // + hôm nay
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
                streakToday.LessonCount += 1;
                _context.UserStreaks.Update(streakToday);
                await _context.SaveChangesAsync();
            }
        }

		public async Task<int> GetCurrentStreakAsOfTodayAsync(Guid userId, TimeZoneInfo tz = null)
		{
			var zone = GetTzOrDefault(tz);
			var today = TodayInTz(zone);

			// Nếu hôm nay có hoạt động -> anchor = hôm nay; nếu không -> anchor = hôm qua
			var hasToday = await _context.UserStreaks
				.AnyAsync(s => s.UserId == userId && s.Date == today);

			var anchor = hasToday ? today : today.AddDays(-1);

			// Nếu không có record ở anchor (tức là hôm nay không làm và hôm qua cũng không làm) => 0
			var hasAnchor = await _context.UserStreaks
				.AnyAsync(s => s.UserId == userId && s.Date == anchor);
			if (!hasAnchor) return 0;

			// Lấy lịch sử ngược về trước tính từ anchor để đếm chuỗi liên tiếp
			var latest = await _context.UserStreaks
				.Where(s => s.UserId == userId && s.Date <= anchor)
				.OrderByDescending(s => s.Date)
				.Take(40)
				.ToListAsync();

			int streak = 0;
			DateTime? prev = null;

			foreach (var s in latest)
			{
				if (prev == null || (prev.Value - s.Date).Days == 1)
				{
					streak++;
					prev = s.Date;
				}
				else break;
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


