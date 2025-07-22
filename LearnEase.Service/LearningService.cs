using LearnEase.Repository;
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

        public async Task<LearningResponse?> GetNextLessonBlockForUserAsync(Guid userId)
        {
            if (userId == Guid.Empty) return null;

            // Lấy cài đặt phương ngữ của user
            var userSettings = (await _userSettingsRepo.GetAllAsync())
                .FirstOrDefault(us => us.UserId == userId);
            Guid? preferredDialectId = userSettings?.PreferredDialectId;

            // Lấy progress của user
            var userProgress = (await _userProgressRepo.GetAllAsync())
                .Where(up => up.UserId == userId).ToList();

            // Lấy dữ liệu lesson + vocab + speaking liên quan
            var lessonRepo = _uow.GetRepository<Lesson>();
            var lessonVocabRepo = _uow.GetRepository<LessonVocabulary>();
            var lessonSpeakingRepo = _uow.GetRepository<LessonSpeaking>();

            var allLessons = await lessonRepo.GetAllAsync();
            if (preferredDialectId.HasValue)
                allLessons = allLessons.Where(l => l.DialectId == preferredDialectId.Value).ToList();

            var allLessonVocabs = await lessonVocabRepo.GetAllAsync();
            var allLessonSpeaking = await lessonSpeakingRepo.GetAllAsync();
            var allVocabs = await _vocabRepo.GetAllAsync();
            var allSpeaking = await _speakingRepo.GetAllAsync();

            foreach (var lesson in allLessons.OrderBy(l => l.Order))
            {
                var lessonVocabs = allLessonVocabs
                    .Where(lv => lv.LessonId == lesson.LessonId)
                    .Select(lv => lv.VocabId)
                    .ToList();

                var lessonSpeakings = allLessonSpeaking
                    .Where(ls => ls.LessonId == lesson.LessonId)
                    .Select(ls => ls.ExerciseId)
                    .ToList();

                var progress = userProgress
                    .Where(p => p.LessonId == lesson.LessonId)
                    .ToList();

                int correctCount = progress.Count(p => p.IsCorrect == true);

                if (correctCount < 8)
                {
                    var vocabItems = allVocabs
                        .Where(v => lessonVocabs.Contains(v.VocabId))
                        .ToList();

                    var speakingItems = allSpeaking
                        .Where(s => lessonSpeakings.Contains(s.ExerciseId))
                        .ToList();

                    return new LearningResponse
                    {
                        LessonId = lesson.LessonId,
                        Title = lesson.Title,
                        Vocabularies = vocabItems,
                        SpeakingExercises = speakingItems
                    };
                }
            }

            return null;
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

        // Hàm này để lấy lesson theo userid và lessonid để track progress user đã học
        public async Task<LearningResponse?> GetLessonBlockByUserAndLessonAsync(Guid userId, Guid lessonId)
        {
            if (userId == Guid.Empty || lessonId == Guid.Empty) return null;

            var lessonRepo = _uow.GetRepository<Lesson>();
            var lessonVocabRepo = _uow.GetRepository<LessonVocabulary>();
            var lessonSpeakingRepo = _uow.GetRepository<LessonSpeaking>();
            var userProgress = (await _userProgressRepo.GetAllAsync())
                               .Where(p => p.UserId == userId && p.LessonId == lessonId)
                               .ToList();

            var vocabIds = (await lessonVocabRepo.GetAllAsync())
                            .Where(lv => lv.LessonId == lessonId)
                            .Select(lv => lv.VocabId)
                            .ToList();

            var speakingIds = (await lessonSpeakingRepo.GetAllAsync())
                              .Where(ls => ls.LessonId == lessonId)
                              .Select(ls => ls.ExerciseId)
                              .ToList();

            var vocabItems = (await _vocabRepo.GetAllAsync())
                            .Where(v => vocabIds.Contains(v.VocabId))
                            .ToList();

            var speakingItems = (await _speakingRepo.GetAllAsync())
                                .Where(s => speakingIds.Contains(s.ExerciseId))
                                .ToList();

            var vocabCorrect = userProgress.Count(p => p.IsCorrect && p.VocabId.HasValue);
            var speakingCorrect = userProgress.Count(p => p.IsCorrect && p.ExerciseId.HasValue);

            var lesson = (await lessonRepo.GetAllAsync()).FirstOrDefault(l => l.LessonId == lessonId);
            if (lesson == null) return null;

            return new LearningResponse
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Vocabularies = vocabItems,
                SpeakingExercises = speakingItems,
                VocabCorrectCount = vocabCorrect,
                SpeakingCorrectCount = speakingCorrect
            };
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







