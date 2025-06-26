using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Request
{
    public class RecordScoreRequest
    {
        public Guid UserId { get; set; }
        public string Period { get; set; } = "weekly"; // hoặc "alltime"
        public int Score { get; set; }
    }
}
