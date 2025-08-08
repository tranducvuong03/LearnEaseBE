using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface IUserStreakService
    {
        Task UpdateStreakAsync(Guid userId);
        Task UpdateStreakAsync(Guid userId, TimeZoneInfo tz = null);
        Task<int> GetCurrentStreakAsOfTodayAsync(Guid userId, TimeZoneInfo tz = null);
        Task<DateTime?> GetLastActiveDateAsync(Guid userId);
    }
}
