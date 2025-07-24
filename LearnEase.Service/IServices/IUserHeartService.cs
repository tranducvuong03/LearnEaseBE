using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface IUserHeartService
    {
            Task<int> GetCurrentHeartsAsync(Guid userId);
            Task<bool> UseHeartAsync(Guid userId, bool isPremium);
            Task<int> RegenerateHeartsAsync(Guid userId);

    }
}