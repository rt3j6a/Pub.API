using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetAllResultsByEventRequest {
        [Required]
        public int EventId { get; set; }
    }
}
