using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class LeaderboardDisplayDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public string Period { get; set; }
        public DateTime RecordedAt { get; set; }
    }

}
