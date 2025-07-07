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
        Task<int> GetCurrentStreakAsync(Guid userId);
        Task<DateTime?> GetLastActiveDateAsync(Guid userId);
    }
}
