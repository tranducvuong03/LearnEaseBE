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
        public async Task<int> GetCurrentHeartsAsync(Guid userId)
        {
            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            return userHeart?.CurrentHearts ?? 0;
        }
        public async Task<bool> UseHeartAsync(Guid userId, bool isPremium)
        {
            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            if (userHeart == null)
                return false;

            if (isPremium)
                return true; // Premium: không trừ tim

            if (userHeart.CurrentHearts <= 0)
                return false; // Hết tim

            userHeart.CurrentHearts -= 1;
            userHeart.LastUsedAt = DateTime.UtcNow;
            userHeart.LastRegeneratedAt ??= DateTime.UtcNow;

            _context.UserHearts.Update(userHeart);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> RegenerateHeartsAsync(Guid userId)
        {
            var userHeart = await _context.UserHearts
                .FirstOrDefaultAsync(uh => uh.UserId == userId);

            if (userHeart == null)
                return 0;

            var now = DateTime.UtcNow;
            var lastRegen = userHeart.LastRegeneratedAt ?? userHeart.LastUsedAt ?? now;
            var hoursElapsed = (now - lastRegen).TotalHours;

            int heartsToRegen = (int)(hoursElapsed / 3);

            if (heartsToRegen <= 0 || userHeart.CurrentHearts >= 5)
                return userHeart.CurrentHearts;

            userHeart.CurrentHearts = Math.Min(5, userHeart.CurrentHearts + heartsToRegen);
            userHeart.LastRegeneratedAt = lastRegen.AddHours(heartsToRegen * 3);

            _context.UserHearts.Update(userHeart);
            await _context.SaveChangesAsync();

            return userHeart.CurrentHearts;
        }
    }
}
