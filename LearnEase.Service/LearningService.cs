﻿using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using LearnEase.Service.Models.Response;

namespace LearnEase.Service
{
    public class LearningService : ILearningService
    {
        private readonly IGenericRepository<UserProgress> _userProgressRepo;
        private readonly IGenericRepository<VocabularyItem> _vocabRepo;
        private readonly IGenericRepository<SpeakingExercise> _speakingRepo;
        private readonly IGenericRepository<UserSettings> _userSettingsRepo;
        private readonly IUnitOfWork _uow;

        public LearningService(IUnitOfWork uow)
        {
            _uow = uow;
            _userProgressRepo = _uow.GetRepository<UserProgress>();
            _vocabRepo = _uow.GetRepository<VocabularyItem>();
            _speakingRepo = _uow.GetRepository<SpeakingExercise>();
            _userSettingsRepo = _uow.GetRepository<UserSettings>();
        }

        public Task<LessonResponse?> GetNextLessonBlockForUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<NextLesson?> GetNextLessonForUserAsync(Guid userId)
        {
            // Lấy cài đặt phương ngữ
            var userSettings = (await _userSettingsRepo.GetAllAsync()).FirstOrDefault(us => us.UserId == userId);
            Guid? preferredDialectId = userSettings?.PreferredDialectId;

            var userProgress = (await _userProgressRepo.GetAllAsync())
                                .Where(up => up.UserId == userId)
                                .ToList();

            IEnumerable<VocabularyItem> allVocabs;
            IEnumerable<SpeakingExercise> allSpeakingExercises;

            if (preferredDialectId.HasValue)
            {
                allVocabs = (await _vocabRepo.GetAllAsync())
                            .Where(v => v.DialectId == preferredDialectId.Value);
                allSpeakingExercises = (await _speakingRepo.GetAllAsync())
                                        .Where(se => se.DialectId == preferredDialectId.Value);
            }
            else
            {
                allVocabs = await _vocabRepo.GetAllAsync();
                allSpeakingExercises = await _speakingRepo.GetAllAsync();
            }

            // 1. Ưu tiên bài Vocabulary trước
            var nextVocabularyItem = allVocabs
                                    .OrderBy(v => v.VocabId)
                                    .FirstOrDefault(v => !userProgress.Any(up => up.VocabId == v.VocabId));

            if (nextVocabularyItem != null)
            {
                return new NextLesson
                {
                    LessonType = "Vocabulary",
                    LessonId = nextVocabularyItem.VocabId,
                    PromptOrWord = nextVocabularyItem.Word,
                    AudioUrl = nextVocabularyItem.AudioUrl,
                    DistractorsJson = nextVocabularyItem.DistractorsJson,
                    DialectId = nextVocabularyItem.DialectId
                };
            }

            // 2. Nếu hết từ vựng mới đến bài Speaking
            var nextSpeakingExercise = allSpeakingExercises
                                        .OrderBy(se => se.ExerciseId)
                                        .FirstOrDefault(se => !userProgress.Any(up => up.ExerciseId == se.ExerciseId));

            if (nextSpeakingExercise != null)
            {
                return new NextLesson
                {
                    LessonType = "Speaking",
                    LessonId = nextSpeakingExercise.ExerciseId,
                    PromptOrWord = nextSpeakingExercise.Prompt,
                    AudioUrl = nextSpeakingExercise.SampleAudioUrl,
                    DialectId = nextSpeakingExercise.DialectId
                };
            }

            return null;
        }

        public async Task<bool> IsLessonCompleted(Guid userId, Guid lessonId)
        {
            var progresses = (await _userProgressRepo.GetAllAsync())
                             .Where(up => up.UserId == userId && up.LessonId == lessonId)
                             .ToList();

            int correctCount = progresses.Count(p => p.IsCorrect == true);
            return correctCount >= 8;
        }

        public async Task<int> GetCompletedLessonCountInTopic(Guid userId, Guid topicId)
        {
            var allLessons = (await _uow.GetRepository<Lesson>().GetAllAsync())
                              .Where(l => l.TopicId == topicId).ToList();

            int count = 0;
            foreach (var lesson in allLessons)
            {
                if (await IsLessonCompleted(userId, lesson.LessonId))
                {
                    count++;
                }
            }
            return count;
        }

        public async Task UpdateTopicProgress(Guid userId, Guid topicId)
        {
            var userTopicProgressRepo = _uow.GetRepository<UserTopicProgress>();
            var existing = (await userTopicProgressRepo.GetAllAsync())
                           .FirstOrDefault(x => x.UserId == userId && x.TopicId == topicId);

            int completedCount = await GetCompletedLessonCountInTopic(userId, topicId);

            if (existing != null)
            {
                existing.CompletedLessonCount = completedCount;
                userTopicProgressRepo.Update(existing);
            }
            else
            {
                await userTopicProgressRepo.AddAsync(new UserTopicProgress
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TopicId = topicId,
                    CompletedLessonCount = completedCount,
                });
            }

            _uow.CommitTransaction();
        }

        /*public async Task<UserLessonResponse?> GetNextLessonBlockForUserAsync(Guid userId)
        {
            var userSettings = (await _userSettingsRepo.GetAllAsync())
                .FirstOrDefault(us => us.UserId == userId);
            Guid? preferredDialectId = userSettings?.PreferredDialectId;

            var userProgress = (await _userProgressRepo.GetAllAsync())
                .Where(up => up.UserId == userId).ToList();

            IEnumerable<VocabularyItem> allVocabs = await _vocabRepo.GetAllAsync();
            IEnumerable<SpeakingExercise> allSpeaking = await _speakingRepo.GetAllAsync();

            if (preferredDialectId.HasValue)
            {
                var dialectId = preferredDialectId.Value;
                allVocabs = allVocabs.Where(v => v.DialectId == dialectId);
                allSpeaking = allSpeaking.Where(s => s.DialectId == dialectId);
            }

            var remainingVocabs = allVocabs
                .Where(v => !userProgress.Any(p => p.VocabId == v.VocabId))
                .OrderBy(v => v.VocabId)
                .Take(5)
                .ToList();

            var remainingSpeaking = allSpeaking
                .Where(s => !userProgress.Any(p => p.ExerciseId == s.ExerciseId))
                .OrderBy(s => s.ExerciseId)
                .Take(5)
                .ToList();

            if (!remainingVocabs.Any() && !remainingSpeaking.Any()) return null;

            return new LessonResponse
            {
                LessonId = Guid.NewGuid(),
                Vocabularies = remainingVocabs,
                SpeakingExercises = remainingSpeaking
            };
        }*/
    }
}
