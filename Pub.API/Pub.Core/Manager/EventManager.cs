using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Common;
using Pub.Core.Interface;
using Pub.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Manager {
    public class EventManager : CoreManager, IEventManager {
        public EventManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddEventAsync(string eventName, string? eventDescription, DateTime eventPinnedDateTime) {
            var eventExists = await provider.Events.AnyAsync(x => x.EventName.Equals(eventName));

            if (eventExists) {
                return (false, Messages.Event.EventAlreadyExistsWithGivenDetails);
            }

            Event newEvent = new Event {
                EventName=eventName,
                EventDescription=eventDescription,
                EventPinnedDateTime=eventPinnedDateTime,
                EventAssignedDate=DateTime.Now,
                EventStatusId=(int)Common.EventStatus.draft
            };

            try {
                await provider.Events.AddAsync(newEvent);
                await provider.SaveChangesAsync();
                return (true, Messages.Event.EventAddedSuccessfully);

            }catch(Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<IEnumerable<object>> GetActiveEventsAsync() {
            return await provider.Events.Where(x=>x.EventStatusId==(int)Common.EventStatus.active).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllEventsAsync() {
            return await provider.Events.ToListAsync();
        }

        public async Task<object?> GetEventAsync(int eventId) {
            return await provider.Events.FirstOrDefaultAsync(x => x.EventId.Equals(eventId));
        }

        public async Task<(bool success, string message)> UpdateEventStatusAsync(int eventId, Common.EventStatus status) {
            var eventToUpdate=await provider.Events.FirstOrDefaultAsync(x=>x.EventId==eventId);

            if (eventToUpdate==null) {
                return (false, Messages.Event.EventDoesntExistsOrAlreadyRemoved);
            }

            eventToUpdate.EventStatusId = (int)status;

            try {
                provider.Events.Update(eventToUpdate);
                await provider.SaveChangesAsync();
                return (true, Messages.Event.EventUpdatedSuccessfully);

            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        
    }
}
