using LearnEase.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface IUserHeartService
    {
            Task<UserHeartDisplayDTO> GetCurrentHeartsAsync(Guid userId);
            Task<bool> UseHeartAsync(Guid userId);

    }
}