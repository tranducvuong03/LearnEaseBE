using LearnEase.Repository.EntityModel;
using System.ComponentModel.DataAnnotations;

public class UserProgress
{
    [Key]
    public Guid ProgressId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public Guid? VocabId { get; set; }
    public Guid? ExerciseId { get; set; }

    [Required]
    public Guid LessonId { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    public User User { get; set; }
    public VocabularyItem? VocabularyItem { get; set; }
    public SpeakingExercise? SpeakingExercise { get; set; }
    public Lesson Lesson { get; set; }
}
