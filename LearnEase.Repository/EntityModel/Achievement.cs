using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Achievement
    {
        [Key]
        public Guid AchievementId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime AchievedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }

}
