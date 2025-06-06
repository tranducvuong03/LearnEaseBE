using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnEase.Repository.EntityModel
{
    public class UserSettings
    {
        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }

        public Guid? PreferredDialectId { get; set; }

        public float? PlaybackSpeed { get; set; }
        public bool NotificationOn { get; set; }

        public User User { get; set; }
    }
}
