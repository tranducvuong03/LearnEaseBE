using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.Models.Request
{
    public class EvaluateLessonRequest
    {
        public Guid UserId { get; set; }
        public Guid LessonId { get; set; }
        public SkillType Skill { get; set; }

        public Dictionary<string, string> Answers { get; set; } = new(); // key: index, value: A/B/C/...
    }

}
