using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.EntityModel
{
    public class UserTopicProgress
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        public int CompletedLessonCount { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }

}
