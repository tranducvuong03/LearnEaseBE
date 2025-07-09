using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Service.Models.Request
{
    public class EvaluateDrillRequest
    {
        [Required]
        public IFormFile AudioFile { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }
    }
}
