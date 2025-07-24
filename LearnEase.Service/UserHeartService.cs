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

        public async Task<int> GetCurrentHeartsAsync(Guid userId)
        {
            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            if (userHeart == null)
                return 0;

            if (userHeart.CurrentHearts >= 5)
                return userHeart.CurrentHearts;

            var now = DateTime.UtcNow;
            var lastRegen = userHeart.LastRegeneratedAt ?? userHeart.LastUsedAt ?? now;
            var minutesElapsed = (now - lastRegen).TotalMinutes;

            int heartsToRegen = (int)(minutesElapsed / 180); // 3 tiếng = 180 phút

            if (heartsToRegen <= 0)
                return userHeart.CurrentHearts;

            userHeart.CurrentHearts = Math.Min(5, userHeart.CurrentHearts + heartsToRegen);
            userHeart.LastRegeneratedAt = lastRegen.AddMinutes(heartsToRegen * 180);

            _context.UserHearts.Update(userHeart);
            await _context.SaveChangesAsync();

            return userHeart.CurrentHearts;
        }



    }
}
