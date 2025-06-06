using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Leaderboard
    {
        [Key]
        public Guid LeaderboardId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Period { get; set; } // e.g., "weekly", "monthly"

        [Required]
        public int Score { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }
}
