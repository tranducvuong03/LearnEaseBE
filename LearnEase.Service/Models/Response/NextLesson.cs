using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class NextLesson
    {
        public string LessonType { get; set; } // "Vocabulary" hoặc "Speaking"
        public Guid LessonId { get; set; }     // ID của VocabularyItem hoặc SpeakingExercise
        public string PromptOrWord { get; set; } // Word nếu là Vocabulary, Prompt nếu là Speaking
        public string? AudioUrl { get; set; }    // AudioUrl cho Vocabulary, SampleAudioUrl cho Speaking
        public string? Meaning { get; set; }     // Chỉ có với Vocabulary (có thể null)
        public Guid DialectId { get; set; }    // Phương ngữ của bài học
    }
}
