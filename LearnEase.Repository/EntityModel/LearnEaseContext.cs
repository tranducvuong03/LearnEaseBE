using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.EntityModel
{
    public class LearnEaseContext : DbContext
    {
        public LearnEaseContext(DbContextOptions<LearnEaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Dialect> Dialects { get; set; }
        public DbSet<VocabularyItem> VocabularyItems { get; set; }
        public DbSet<SpeakingExercise> SpeakingExercises { get; set; }
        public DbSet<SpeakingAttempt> SpeakingAttempts { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonVocabulary> LessonVocabularies { get; set; }
        public DbSet<LessonSpeaking> LessonSpeakings { get; set; }
        public DbSet<AiLesson> AiLessons { get; set; }
        public DbSet<AiLessonPart> AiLessonParts { get; set; }
        public DbSet<UserLessonAttempt> UserLessons { get; set; }
        public DbSet<UserStreak> UserStreaks { get; set; }
        public DbSet<TransactionLogs> TransactionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // LessonSpeaking composite key
            modelBuilder.Entity<LessonSpeaking>()
                .HasKey(ls => new { ls.LessonId, ls.ExerciseId });

            modelBuilder.Entity<LessonSpeaking>()
                .HasOne(ls => ls.Lesson)
                .WithMany(l => l.LessonSpeakings)
                .HasForeignKey(ls => ls.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonSpeaking>()
                .HasOne(ls => ls.SpeakingExercise)
                .WithMany(se => se.LessonSpeakings)
                .HasForeignKey(ls => ls.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 Quan trọng để tránh multiple cascade

            // LessonVocabulary composite key
            modelBuilder.Entity<LessonVocabulary>()
                .HasKey(lv => new { lv.LessonId, lv.VocabId });

            modelBuilder.Entity<LessonVocabulary>()
                .HasOne(lv => lv.Lesson)
                .WithMany(l => l.LessonVocabularies)
                .HasForeignKey(lv => lv.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonVocabulary>()
                .HasOne(lv => lv.VocabularyItem)
                .WithMany(v => v.LessonVocabularies)
                .HasForeignKey(lv => lv.VocabId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.SpeakingExercise)
                .WithMany(se => se.UserProgresses)
                .HasForeignKey(up => up.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Topic)
                .WithMany(t => t.Lessons)
                .HasForeignKey(l => l.TopicId);

        }
    }
}
