using System;
using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Topic
    {
        [Key]
        public Guid TopicId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }
        public int Order { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
