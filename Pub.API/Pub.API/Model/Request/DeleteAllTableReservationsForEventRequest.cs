using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class DeleteAllTableReservationsForEventRequest {
        [Required]
        public int EventId { get; set; }
    }
}
