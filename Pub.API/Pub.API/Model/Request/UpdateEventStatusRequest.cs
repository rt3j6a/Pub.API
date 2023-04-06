using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdateEventStatusRequest {
        [Required]
        public int eventId { get; set; }
        [Required]
        public int eventStatusValue { get; set; }
    }
}
