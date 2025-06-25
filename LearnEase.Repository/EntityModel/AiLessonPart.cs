using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Repository.EntityModel
{
    public class AiLessonPart
    {
        [Key]
        public Guid PartId { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        [ForeignKey("LessonId")]
        public AiLesson Lesson { get; set; }

        [Required]
        public SkillType Skill { get; set; }

        [Required]
        public string Prompt { get; set; }

        public string? ReferenceText { get; set; }
        public string? AudioUrl { get; set; }
        public string? ChoicesJson { get; set; }
    }
}
