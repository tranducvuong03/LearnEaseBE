using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Request
{
    public class SubmitProgressRequest
    {
        public Guid LessonId { get; set; }
        public Guid? VocabId { get; set; }
        public Guid? ExerciseId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
