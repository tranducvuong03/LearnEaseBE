using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class SpeakingAttempt
    {
        [Key]
        public Guid AttemptId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }

        [Required]
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        public float Score { get; set; }

        public string? UserAudioUrl { get; set; }

        public string? Transcription { get; set; }

        public User User { get; set; }
        public SpeakingExercise Exercise { get; set; }
    }
}
