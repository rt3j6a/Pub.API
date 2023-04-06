using Pub.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface ITeamManager {
        Task<IEnumerable<object>> GetAllTeamAssignmentsAsync();

        Task<IEnumerable<object>> GetActiveTeamAssignmentsAsync();

        Task<(bool success, string message)> AddTeamAssignmentAsync(string teamName, decimal teamMemberCount, string sourceEmailAddress, int eventId);

        Task<(bool success, string message)> UpdateTeamAssignmentStatusAsync(int assignmentId, Common.TeamAssignmentStatus status);
    }
}
