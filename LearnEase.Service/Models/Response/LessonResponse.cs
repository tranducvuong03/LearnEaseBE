using LearnEase.Repository.EntityModel;

namespace LearnEase.Service.Models.Response
{
    public class LessonModel
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; } = "Lesson Block";

        public List<VocabularyItem> Vocabularies { get; set; }
        public List<SpeakingExercise> SpeakingExercises { get; set; }
    }

}
