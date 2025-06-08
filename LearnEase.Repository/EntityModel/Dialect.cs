using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Dialect
    {
        [Key]
        public Guid DialectId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Region { get; set; }

        public string? AccentCode { get; set; }

        public string? Description { get; set; }

        public string? VoiceSampleUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Language Language { get; set; }

        public ICollection<VocabularyItem> VocabularyItems { get; set; }
        public ICollection<SpeakingExercise> SpeakingExercises { get; set; }
    }
}
