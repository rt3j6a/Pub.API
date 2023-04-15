using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IQuestionManager {
        Task<(bool success, string message)> AddQuestionAsync(string questionContent, int eventId);

        Task<(bool success, string message)> UpdateQuestionAsync(int questionId,string questionContent);

        Task<(bool success, string message)> RemoveQuestionAsync(int questionId);

        Task<IEnumerable<object>> GetAllQuestionsAsync();

        Task<IEnumerable<object>> GetAllQuestionsForEventAsync(int eventId);

        Task<object?> GetQuestionAsync(int questionId);
    }
}
