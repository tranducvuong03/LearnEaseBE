using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Repository.DTO
{
    public class EvaluateAccentRequest
    {
        public IFormFile AudioFile { get; set; }
        public Guid DialectId { get; set; }
    }

}
