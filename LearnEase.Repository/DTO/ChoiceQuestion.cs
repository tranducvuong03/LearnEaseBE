using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearnEase.Repository.DTO
{
    public class ChoiceQuestion
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("choices")]
        public List<string> Choices { get; set; }

        [JsonPropertyName("answer")]
        public string Answer { get; set; }
    }

}
