using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string? Password { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserSettings? Settings { get; set; }
        public ICollection<SpeakingAttempt> SpeakingAttempts { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }
        public ICollection<Leaderboard> Leaderboards { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
    }
}
