using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Repository.EntityModel
{
    public class UserLessonAttempt
    {
        [Key]
        public Guid AttemptId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        [Required]
        public SkillType Skill { get; set; }

        public string? UserAnswer { get; set; }
        public float Score { get; set; }
        public string? Feedback { get; set; }
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("LessonId")]
        public AiLesson Lesson { get; set; }
    }
}
