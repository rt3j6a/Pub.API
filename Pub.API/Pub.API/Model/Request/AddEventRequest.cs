using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddEventRequest {

        [Required]
        public string EventName { get; set; } = string.Empty;

        public string? EventDescription { get; set; }

        [Required]
        public DateTime EventPinnedDateTime { get; set; }
    }
}
