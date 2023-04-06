using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Common;
using Pub.Core.Interface;
using Pub.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Manager {
    public class TeamManager : CoreManager, ITeamManager {
        public TeamManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddTeamAssignmentAsync(string teamName, decimal teamMemberCount, string sourceEmailAddress, int eventId) {
            var exists = await provider.TeamAssignments.AnyAsync(x => x.TeamName.Equals(teamName) || x.SourceEmailAddress.Equals(sourceEmailAddress));

            if (exists) { 
                return (false, Messages.Team.TeamAlreadyRegistered);

            }

            if (!(teamMemberCount > 0)) {
                return (false, Messages.Team.TeamMemberCountInvalid);
            }

            TeamAssignment assignment = new TeamAssignment{ 
                TeamName= teamName,
                SourceEmailAddress= sourceEmailAddress,
                EventId=eventId,
                TeamMemberCount=decimal.ToInt32(teamMemberCount),
                TeamAssignmentStatusId=(int)Common.TeamAssignmentStatus.active
            };

            try {
                await provider.TeamAssignments.AddAsync(assignment);
                await provider.SaveChangesAsync();
                return (true, Messages.Team.AssignmentAdded);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<IEnumerable<object>> GetActiveTeamAssignmentsAsync() {
            return await provider.TeamAssignments.Where(x=>x.TeamAssignmentStatusId==(int)Common.TeamAssignmentStatus.active).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllTeamAssignmentsAsync() {
            return await provider.TeamAssignments.ToListAsync();
        }

        public async Task<(bool success, string message)> UpdateTeamAssignmentStatusAsync(int assignmentId, Common.TeamAssignmentStatus status) {
            var assignment=await provider.TeamAssignments.FirstOrDefaultAsync(x=>x.TeamAssignmentId==assignmentId);

            if (assignment==null) {
                return (false, Messages.Team.TeamAssignmentDoestnExistsOrDeleted);
            }

            assignment.TeamAssignmentStatusId = (int)assignmentId;

            try {
                provider.TeamAssignments.Update(assignment);
                await provider.SaveChangesAsync();
                return (true, Messages.Team.AssignmentStatusUpdatedSuccessfully);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }
    }
}
