using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class VocabularyItem
    {
        [Key]
        public Guid VocabId { get; set; }

        [Required]
        public Guid DialectId { get; set; }

        [Required, MaxLength(100)]
        public string Word { get; set; }

        [Required]
        public string Meaning { get; set; }

        public string? AudioUrl { get; set; }

        [MaxLength(500)]
        public string? DistractorsJson { get; set; }
        public Dialect Dialect { get; set; }

        public ICollection<UserProgress> UserProgresses { get; set; }
    }
}
