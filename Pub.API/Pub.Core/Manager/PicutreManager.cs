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
    public class PictureManager : CoreManager, IPictureManager {
        public PictureManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddPictureEventLinkAsync(int eventId) {
            var exists = await provider.Events.AnyAsync(x => x.EventId == eventId);

            if (!exists) {
                return (false, Messages.Event.EventDoesntExistsOrAlreadyRemoved);
            }

            var link=await provider.PicutreEventLinks.FirstOrDefaultAsync(x=>x.EventId == eventId);

            if (link!=null) {
                return (true, link.PicturesSourceRoute);
            }

            string today = string.Format("{0}_{1}_{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string route = string.Format("event_{0} {1}", eventId, today);

            PicutreEventLink newLink = new PicutreEventLink { 
                EventId = eventId,
                PicturesSourceRoute=route
            };

            await provider.PicutreEventLinks.AddAsync(newLink);
            await provider.SaveChangesAsync();

            return (true, newLink.PicturesSourceRoute);
        }

        public async Task<string?> GetSoruceRouteForEventAsync(int eventId) {
            var link=await provider.PicutreEventLinks.FirstOrDefaultAsync(x=>x.EventId == eventId);

            if (link == null) {
                return null;
            }

            return link.PicturesSourceRoute;
        }
    }
}
