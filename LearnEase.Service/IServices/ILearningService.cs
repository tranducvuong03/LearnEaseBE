﻿using LearnEase.Service.Models.Response;

namespace LearnEase.Service.IServices
{
    public interface ILearningService
    {
        Task<NextLesson?> GetNextLessonForUserAsync(Guid userId);
        Task<LessonResponse?> GetNextLessonBlockForUserAsync(Guid userId);
        Task<int> GetCompletedLessonCountInTopic(Guid userId, Guid topicId);
        Task UpdateTopicProgress(Guid userId, Guid topicId);

    }
}
