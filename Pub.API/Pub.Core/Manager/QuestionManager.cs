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
    public class QuestionManager : CoreManager, IQuestionManager {
        public QuestionManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddQuestionAsync(string questionContent, int eventId) {
            EventLikedQuestion question = new EventLikedQuestion { 
                EventId = eventId,
                QuestionContent = questionContent
            };

            await provider.EventLikedQuestions.AddAsync(question);
            await provider.SaveChangesAsync();

            return (true, Messages.Question.QuestionAddedSuccessfully);
        }

        public async Task<IEnumerable<object>> GetAllQuestionsAsync() {
            return await provider.EventLikedQuestions.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllQuestionsForEventAsync(int eventId) {
            return await provider.EventLikedQuestions.Where(x=>x.EventId == eventId).ToListAsync();
        }

        public async Task<object?> GetQuestionAsync(int questionId) {
            return await provider.EventLikedQuestions.FirstOrDefaultAsync(x=>x.EventLikedQuestionId == questionId);
        }

        public async Task<(bool success, string message)> RemoveQuestionAsync(int questionId) {
            var result = await provider.EventLikedQuestions.Where(x => x.EventLikedQuestionId == questionId).ExecuteDeleteAsync();

            if (result == 0) {
                return (false, Messages.Question.QuestionDoesntExistsOrAlreadyRemoved);
            }

            return (true, Messages.Question.QuestionDeletedSuccessfully);
        }

        public async Task<(bool success, string message)> UpdateQuestionAsync(int questionId, string questionContent) {
            var question = await provider.EventLikedQuestions.FirstOrDefaultAsync(x => x.EventLikedQuestionId == questionId);

            if (question == null) {
                return (false, Messages.Question.QuestionDoesntExistsOrAlreadyRemoved);
            }
            
            question.QuestionContent = questionContent;
            provider.EventLikedQuestions.Update(question);
            await provider.SaveChangesAsync();

            return (true, Messages.Question.QuestionUpdatedSuccessfully);
        }
    }
}
