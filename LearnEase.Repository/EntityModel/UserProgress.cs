using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class UserProgress
    {
        [Key]
        public Guid ProgressId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Guid? VocabId { get; set; }
        public Guid? ExerciseId { get; set; }

        public DateTime LastReviewed { get; set; } = DateTime.UtcNow;

        public int RepetitionCount { get; set; } = 0;

        public User User { get; set; }
        public VocabularyItem? VocabularyItem { get; set; }
        public SpeakingExercise? SpeakingExercise { get; set; }
    }
}
