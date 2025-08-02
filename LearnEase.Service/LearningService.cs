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
            if (userId == Guid.Empty)
                return null;

            // 1. Lấy Preferred Dialect từ user
            var userSettings = (await _userSettingsRepo.GetAllAsync())
                .FirstOrDefault(us => us.UserId == userId);

            Guid? preferredDialectId = userSettings?.PreferredDialectId;

            // 2. Lấy progress
            var userProgress = (await _userProgressRepo.GetAllAsync())
                .Where(up => up.UserId == userId)
                .ToList();

            // 3. Lấy dữ liệu lessons + vocab + speaking
            var lessonRepo = _uow.GetRepository<Lesson>();
            var lessonVocabRepo = _uow.GetRepository<LessonVocabulary>();
            var lessonSpeakingRepo = _uow.GetRepository<LessonSpeaking>();

            var allLessons = await lessonRepo.GetAllAsync();
            if (preferredDialectId.HasValue)
                allLessons = allLessons.Where(l => l.DialectId == preferredDialectId.Value).ToList();

            var allLessonVocabs = await lessonVocabRepo.GetAllAsync();
            var allLessonSpeakings = await lessonSpeakingRepo.GetAllAsync();
            var allVocabs = await _vocabRepo.GetAllAsync();
            var allSpeakings = await _speakingRepo.GetAllAsync();

            foreach (var lesson in allLessons.OrderBy(l => l.Order))
            {
                var lessonVocabIds = allLessonVocabs
                    .Where(lv => lv.LessonId == lesson.LessonId)
                    .Select(lv => lv.VocabId)
                    .ToList();

                var lessonSpeakingIds = allLessonSpeakings
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
                        .Where(v => lessonVocabIds.Contains(v.VocabId))
                        .ToList();

                    var speakingItems = allSpeakings
                        .Where(s => lessonSpeakingIds.Contains(s.ExerciseId))
                        .ToList();

                    var vocabResponses = vocabItems.Select(v => new VocabularyResponse
                    {
                        VocabId = v.VocabId,
                        Word = v.Word,
                        AudioUrl = v.AudioUrl,
                        ImageUrl = v.ImageUrl,
                        DistractorsJson = v.DistractorsJson
                    }).ToList();

                    var speakingResponses = speakingItems.Select(s => new SpeakingExerciseResponse
                    {
                        ExerciseId = s.ExerciseId,
                        Prompt = s.Prompt,
                        SampleAudioUrl = s.SampleAudioUrl,
                        ReferenceText = s.ReferenceText
                    }).ToList();

                    return new LearningResponse
                    {
                        LessonId = lesson.LessonId,
                        Title = lesson.Title,
                        Vocabularies = vocabResponses,
                        SpeakingExercises = speakingResponses,
                        VocabCorrectCount = progress.Count(p => p.IsCorrect && p.VocabId.HasValue),
                        SpeakingCorrectCount = progress.Count(p => p.IsCorrect && p.ExerciseId.HasValue)
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
            if (userId == Guid.Empty || lessonId == Guid.Empty)
                return null;

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

            var lesson = (await lessonRepo.GetAllAsync())
                         .FirstOrDefault(l => l.LessonId == lessonId);

            if (lesson == null)
                return null;

            var vocabResponses = vocabItems.Select(v => new VocabularyResponse
            {
                VocabId = v.VocabId,
                Word = v.Word,
                AudioUrl = v.AudioUrl,
                ImageUrl = v.ImageUrl,
                DistractorsJson = v.DistractorsJson
            }).ToList();

            var speakingResponses = speakingItems.Select(s => new SpeakingExerciseResponse
            {
                ExerciseId = s.ExerciseId,
                Prompt = s.Prompt,
                SampleAudioUrl = s.SampleAudioUrl,
                ReferenceText = s.ReferenceText
            }).ToList();

            return new LearningResponse
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Vocabularies = vocabResponses,
                SpeakingExercises = speakingResponses,
                VocabCorrectCount = vocabCorrect,
                SpeakingCorrectCount = speakingCorrect
            };
        }

        public async Task<bool> SubmitUserProgressAsync(Guid userId, Guid lessonId, Guid? vocabId, Guid? exerciseId, bool isCorrect)
        {
            if (userId == Guid.Empty || lessonId == Guid.Empty || (vocabId == null && exerciseId == null))
                return false;

            var existing = (await _userProgressRepo.GetAllAsync())
                .FirstOrDefault(p =>
                    p.UserId == userId &&
                    p.LessonId == lessonId &&
                    ((vocabId != null && p.VocabId == vocabId) || (exerciseId != null && p.ExerciseId == exerciseId)));

            if (existing != null)
            {
                existing.IsCorrect = isCorrect;
                _userProgressRepo.Update(existing);
            }
            else
            {
                var progress = new UserProgress
                {
                    ProgressId = Guid.NewGuid(),
                    UserId = userId,
                    LessonId = lessonId,
                    VocabId = vocabId,
                    ExerciseId = exerciseId,
                    IsCorrect = isCorrect
                };

                await _userProgressRepo.AddAsync(progress);
            }

            await _uow.SaveAsync();
            _uow.CommitTransaction();

            return true;
        }
    }
}







