using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.Models.Response
{
    public class LearningResponse
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }

        public List<VocabularyResponse> Vocabularies { get; set; }
        public List<SpeakingExerciseResponse> SpeakingExercises { get; set; }

        public int VocabCorrectCount { get; set; }
        public int SpeakingCorrectCount { get; set; }

        public int VocabTotal => Vocabularies?.Count ?? 0;
        public int SpeakingTotal => SpeakingExercises?.Count ?? 0;
    }
}
