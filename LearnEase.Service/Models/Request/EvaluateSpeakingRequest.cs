using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Service.Models.Request
{
    public class EvaluateSpeakingRequest
    {
        public IFormFile AudioFile { get; set; }
        public string OriginalPrompt { get; set; }
    }
}
