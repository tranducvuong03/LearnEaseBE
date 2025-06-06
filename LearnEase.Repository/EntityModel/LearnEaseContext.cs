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
    }
}
