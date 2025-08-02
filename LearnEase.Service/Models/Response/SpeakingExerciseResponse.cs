using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class SpeakingExerciseResponse
    {
        public Guid ExerciseId { get; set; }
        public string Prompt { get; set; }
        public string? SampleAudioUrl { get; set; }
        public string? ReferenceText { get; set; }
    }
}
