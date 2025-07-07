using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class UserStreak
    {
        [Key]
        public Guid StreakId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        public int LessonCount { get; set; } = 1;

        public User User { get; set; }
    }
}
