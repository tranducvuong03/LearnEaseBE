using LearnEase.Repository.DTO;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LearnEase.Service
{
    public class UserHeartService : IUserHeartService
    {
        private readonly LearnEaseContext _context;

        public UserHeartService(LearnEaseContext context)
        {
            _context = context;
        }

        public async Task<bool> UseHeartAsync(Guid userId)
        {
            var hasSubscription = await _context.Subscriptions
                .AnyAsync(s => s.UserId == userId && (s.EndDate == null || s.EndDate > DateTime.UtcNow));

            if (hasSubscription)
                return true; // Premium: không trừ tim

            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            if (userHeart == null)
                return false;

            if (userHeart.CurrentHearts <= 0)
                return false;

            userHeart.CurrentHearts -= 1;
            userHeart.LastUsedAt = DateTime.UtcNow;
            userHeart.LastRegeneratedAt ??= DateTime.UtcNow;

            _context.UserHearts.Update(userHeart);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserHeartDisplayDTO> GetCurrentHeartsAsync(Guid userId)
        {
            bool isPremium = await _context.Subscriptions
         .AnyAsync(s => s.UserId == userId && (s.EndDate == null || s.EndDate > DateTime.UtcNow));

            if (isPremium)
            {
                return new UserHeartDisplayDTO
                {
                    CurrentHearts = 5,
                    IsPremium = true,
                    LastUsedAt = null,
                    LastRegeneratedAt = null,
                    MinutesUntilNextHeart = 0
                };
            }

            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            if (userHeart == null)
            {
                return new UserHeartDisplayDTO
                {
                    CurrentHearts = 0,
                    IsPremium = false,
                    LastUsedAt = null,
                    LastRegeneratedAt = null,
                    MinutesUntilNextHeart = 0
                };
            }

            var now = DateTime.UtcNow;
            var lastRegen = userHeart.LastRegeneratedAt ?? userHeart.LastUsedAt ?? now;
            var minutesElapsed = (now - lastRegen).TotalMinutes;

            int heartsToRegen = (int)(minutesElapsed / 180); // 3 tiếng = 180 phút

            if (userHeart.CurrentHearts < 5 && heartsToRegen > 0)
            {
                userHeart.CurrentHearts = Math.Min(5, userHeart.CurrentHearts + heartsToRegen);
                userHeart.LastRegeneratedAt = lastRegen.AddMinutes(heartsToRegen * 180);

                _context.UserHearts.Update(userHeart);
                await _context.SaveChangesAsync();
            }

            int minutesUntilNext = 0;
            if (userHeart.CurrentHearts < 5)
            {
                double remaining = 180 - (now - (userHeart.LastRegeneratedAt ?? lastRegen)).TotalMinutes;
                minutesUntilNext = Math.Max(0, (int)Math.Ceiling(remaining));
            }

            return new UserHeartDisplayDTO
            {
                CurrentHearts = userHeart.CurrentHearts,
                IsPremium = false,
                LastUsedAt = userHeart.LastUsedAt,
                LastRegeneratedAt = userHeart.LastRegeneratedAt,
                MinutesUntilNextHeart = minutesUntilNext
            };
        }



    }
}
