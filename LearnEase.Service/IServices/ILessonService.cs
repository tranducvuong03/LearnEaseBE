using LearnEase.Service.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface ILessonService
    {
        Task<List<LessonResponse>> GetLessonsByTopicAsync(Guid topicId);
    }
}
