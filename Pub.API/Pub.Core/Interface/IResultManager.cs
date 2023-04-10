using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IResultManager {
        Task<(bool success, string message)> AddResultAsync(string teamName, float teamScore, int eventId);

        Task<(bool success, string message)> RemoveResultAsync(int resultId);

        Task<(bool success, string messsage)> UpdateResultScoreAsync(int resultId, float teamScore);

        Task<IEnumerable<object>> GetAllResultsAsync();

        Task<IEnumerable<object>> GetResultsByEventAsync(int eventId);

        Task<object?> GetResultAsync(int resultId);
    }
}
