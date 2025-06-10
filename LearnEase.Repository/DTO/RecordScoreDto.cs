using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.DTO
{
    public class RecordScoreDto
    {
        public Guid UserId { get; set; }
        public string Period { get; set; } = "weekly"; // hoặc "alltime"
        public int Score { get; set; }
    }
}
