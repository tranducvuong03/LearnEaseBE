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
        // Có thể thêm repo cho UserSettings để lấy PreferredDialect
        private readonly IGenericRepository<UserSettings> _userSettingsRepo;

        private readonly IUnitOfWork _uow; // Thêm UnitOfWork nếu bạn muốn thực hiện thao tác lưu

        public LearningService(IUnitOfWork uow)
        {
            _uow = uow;
            _userProgressRepo = _uow.GetRepository<UserProgress>();
            _vocabRepo = _uow.GetRepository<VocabularyItem>();
            _speakingRepo = _uow.GetRepository<SpeakingExercise>();
            _userSettingsRepo = _uow.GetRepository<UserSettings>(); // Khởi tạo nếu cần
        }

        public async Task<NextLesson?> GetNextLessonForUserAsync(Guid userId)
        {
            // Lấy cài đặt người dùng để biết phương ngữ ưu tiên
            var userSettings = (await _userSettingsRepo.GetAllAsync()).FirstOrDefault(us => us.UserId == userId);
            Guid? preferredDialectId = userSettings?.PreferredDialectId;

            // Lấy tất cả tiến trình của người dùng
            var userProgress = (await _userProgressRepo.GetAllAsync())
                                .Where(up => up.UserId == userId)
                                .ToList();

            // Lấy tất cả từ vựng và bài tập nói, có thể lọc theo phương ngữ ưu tiên nếu có
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
                // Nếu không có phương ngữ ưu tiên, lấy tất cả
                allVocabs = await _vocabRepo.GetAllAsync();
                allSpeakingExercises = await _speakingRepo.GetAllAsync();
            }

            // --- Logic đề xuất bài học ---
            // Đây là phần bạn sẽ tùy chỉnh để tạo ra luồng học tập thông minh.
            // Ví dụ đơn giản: Luân phiên giữa Speaking và Vocabulary, ưu tiên bài chưa học.

            // 1. Tìm một bài Speaking chưa học
            var nextSpeakingExercise = allSpeakingExercises
                                        .OrderBy(se => se.ExerciseId) // Sắp xếp để có thứ tự cố định
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

            // 2. Nếu không có bài Speaking nào chưa học, tìm một từ vựng chưa học
            var nextVocabularyItem = allVocabs
                                    .OrderBy(v => v.VocabId) // Sắp xếp để có thứ tự cố định
                                    .FirstOrDefault(v => !userProgress.Any(up => up.VocabId == v.VocabId));

            if (nextVocabularyItem != null)
            {
                return new NextLesson
                {
                    LessonType = "Vocabulary",
                    LessonId = nextVocabularyItem.VocabId,
                    PromptOrWord = nextVocabularyItem.Word,
                    AudioUrl = nextVocabularyItem.AudioUrl,
                    Meaning = nextVocabularyItem.Meaning,
                    DialectId = nextVocabularyItem.DialectId
                };
            }

            // 3. Nếu không còn bài mới nào, bạn có thể implement logic ôn tập ở đây
            // Ví dụ: Tìm bài đã học lâu nhất hoặc bài có điểm thấp nhất để ôn lại.
            // Hiện tại, chỉ trả về null nếu hết bài mới.
            return null; // Không còn bài học nào mới để đề xuất
        }
    }
}
