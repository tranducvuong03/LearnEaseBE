using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public Guid DialectId { get; set; }

        [Required]
        public int Order { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public Topic Topic { get; set; }
        public Dialect Dialect { get; set; }

        public ICollection<LessonVocabulary> LessonVocabularies { get; set; }
        public ICollection<LessonSpeaking> LessonSpeakings { get; set; }
    }
}
