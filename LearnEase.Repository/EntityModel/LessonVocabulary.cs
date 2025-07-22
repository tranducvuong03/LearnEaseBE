using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class LessonVocabulary
    {
        [Required]
        public Guid LessonId { get; set; }

        [Required]
        public Guid VocabId { get; set; }

        public Lesson Lesson { get; set; }
        public VocabularyItem VocabularyItem { get; set; }
    }
}
