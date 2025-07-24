using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class UserHeart
    {
        public Guid UserHeartId { get; set; }
        public Guid UserId { get; set; }

        public int CurrentHearts { get; set; } = 5;
        public DateTime? LastUsedAt { get; set; }
        public DateTime? LastRegeneratedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }

}
