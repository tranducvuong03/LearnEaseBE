using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.DTO
{
    public class UserHeartDisplayDTO
    {
        public int CurrentHearts { get; set; }
        public bool IsPremium { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public DateTime? LastRegeneratedAt { get; set; }
        public int MinutesUntilNextHeart { get; set; }
    }
}
