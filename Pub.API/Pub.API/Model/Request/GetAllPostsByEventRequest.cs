using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetAllPostsByEventRequest {
        [Required]
        public int EventId { get; set; }
    }
}
