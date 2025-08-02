using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class TopicProgressResponse
    {
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int ProgressPercent => TotalLessons == 0 ? 0 : CompletedLessons * 100 / TotalLessons;
        public string Status => ProgressPercent == 100 ? "Completed" : "In Progress";
    }

}
