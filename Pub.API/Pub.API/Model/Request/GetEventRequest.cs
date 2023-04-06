using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetEventRequest {

        [Required]
        public int eventId { get; set; }
    }
}
