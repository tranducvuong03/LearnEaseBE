using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class AiLesson
    {
        [Key]
        public Guid LessonId { get; set; }

        [Required]
        public string Topic { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int DayIndex { get; set; }

        public ICollection<AiLessonPart> Parts { get; set; } // Optional, nhưng tốt cho navigation
    }
}
