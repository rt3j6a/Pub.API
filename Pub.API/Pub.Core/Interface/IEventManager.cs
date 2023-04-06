using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IEventManager {
        Task<IEnumerable<object>> GetAllEventsAsync();

        Task<IEnumerable<object>> GetActiveEventsAsync();
        Task<(bool success, string message)> AddEventAsync(string eventName, string? eventDescription, DateTime eventPinnedDate);

        Task<(bool success, string message)> UpdateEventStatusAsync(int eventId, Common.EventStatus status);

        Task<object?> GetEventAsync(int eventId);


        //Ez később ha kell
        //Task<(bool success, string message)> UpdateEventData();

       // Task<(bool success, string message)> RemoveEvent();
    }
}
