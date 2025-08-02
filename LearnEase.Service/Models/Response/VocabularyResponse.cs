using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class VocabularyResponse
    {
        public Guid VocabId { get; set; }
        public string Word { get; set; }
        public string? AudioUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? DistractorsJson { get; set; }
    }

}
