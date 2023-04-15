using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface IPictureManager {

        Task<(bool success, string message)> AddPictureEventLinkAsync(int eventId);

        Task<string?> GetSoruceRouteForEventAsync(int eventId);


    }
}
