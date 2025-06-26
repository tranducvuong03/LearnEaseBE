using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }

        [Required]
        public Guid DialectId { get; set; }

        [Required]
        public int Order { get; set; } // mốc số 1, 2, 3,...

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }
        public string? IconUrl { get; set; }

        public Dialect Dialect { get; set; }
        public ICollection<LessonVocabulary> LessonVocabularies { get; set; }
        public ICollection<LessonSpeaking> LessonSpeakings { get; set; }
    }

}
