using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class SpeakingExercise
    {
        [Key]
        public Guid ExerciseId { get; set; }

        [Required]
        public Guid DialectId { get; set; }

        [Required]
        public string Prompt { get; set; }

        public string? SampleAudioUrl { get; set; }

        public string? ReferenceText { get; set; }

        public Dialect Dialect { get; set; }

        public ICollection<SpeakingAttempt> Attempts { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; }
    }
}
