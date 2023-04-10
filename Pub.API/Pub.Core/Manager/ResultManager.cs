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
    public class ResultManager : CoreManager, IResultManager {
        public ResultManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddResultAsync(string teamName, float teamScore, int eventId) {
            var exists = await provider.Results.AnyAsync(x => x.TeamName.Equals(teamName) && x.EventId==eventId);

            if (exists) {
                return (false, Messages.Result.ResultForTeamAlreadyAssigned);
            }

            if (teamScore <= 0) {
                return (false, Messages.Result.ScoreMustBeGreaterThanZero);
            }

            Result result = new Result {
                TeamName = teamName,
                TeamScore = teamScore,
                EventId = eventId
            };

           
            await provider.AddAsync(result);
            await provider.SaveChangesAsync();
            return (true, Messages.Result.ResultAssignedSuccessfully);
           
        }

        public async Task<IEnumerable<object>> GetAllResultsAsync() {
            return await provider.Results.ToListAsync();
        }

        public async Task<object?> GetResultAsync(int resultId) {
            return await provider.Results.FirstOrDefaultAsync(x => x.ResultId==resultId);
        }

        public async Task<IEnumerable<object>> GetResultsByEventAsync(int eventId) {
            return await provider.Results.Where(x=> x.EventId==eventId).ToListAsync();
        }


        public async Task<(bool success, string message)> RemoveResultAsync(int resultId) {
            var result = await provider.Results.Where(x => x.ResultId == resultId).ExecuteDeleteAsync();

            await provider.SaveChangesAsync();
            return result == 1 ? (true, Messages.Result.ResultDeletedSuccessfully) : (false, Messages.Result.ResultDoestnExistOrAlreadyDeleted);
            
        }

        public async Task<(bool success, string messsage)> UpdateResultScoreAsync(int resultId, float teamScore) {
            var result= await provider.Results.FirstOrDefaultAsync(x => x.ResultId == resultId);

            if (result==null) {
                return (false, Messages.Result.ResultDoestnExistOrAlreadyDeleted);
            }

            result.TeamScore = teamScore;
           
            provider.Results.Update(result);
            await provider.SaveChangesAsync();
            return (true, Messages.Result.ResultTeamScoreUpdatedSuccessfully);
        }
    }
}
