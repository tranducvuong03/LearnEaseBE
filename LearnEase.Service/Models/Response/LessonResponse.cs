namespace LearnEase.Service.Models.Response
{
    public class LessonResponse
    {
        public Guid LessonId { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
