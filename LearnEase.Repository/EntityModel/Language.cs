using System.ComponentModel.DataAnnotations;

namespace LearnEase.Repository.EntityModel
{
    public class Language
    {
        [Key]
        public Guid LanguageId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Dialect> Dialects { get; set; }
    }
}
