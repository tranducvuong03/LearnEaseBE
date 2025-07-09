using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class SpeakingPracticeHistory
    {
        [Key]
        public Guid PracticeId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid DialectId { get; set; }

        [Required]
        public string Text { get; set; }
        public int Level { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Dialect Dialect { get; set; }
    }

}
