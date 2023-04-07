using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetTableReservationRequest {
        [Required]
        public int ReservationId { get; set; }
    }
}
