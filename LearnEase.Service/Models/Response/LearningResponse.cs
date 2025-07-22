using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.Models.Response
{
    public class LearningResponse
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }

        public List<VocabularyItem> Vocabularies { get; set; }
        public List<SpeakingExercise> SpeakingExercises { get; set; }

        public int VocabCorrectCount { get; set; }      // Số từ đúng
        public int SpeakingCorrectCount { get; set; }   // Số bài nói đúng

        public int VocabTotal => Vocabularies?.Count ?? 0;
        public int SpeakingTotal => SpeakingExercises?.Count ?? 0;
    }


}
