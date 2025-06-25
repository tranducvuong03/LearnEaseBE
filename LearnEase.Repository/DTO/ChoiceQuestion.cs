using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Repository.DTO
{
    public class ChoiceQuestion
    {
        public string Question { get; set; }
        public List<string> Choices { get; set; }
        public string Answer { get; set; }
    }

}
