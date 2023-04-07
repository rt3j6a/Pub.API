using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdateTableReservationCommentRequest {

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public int ReservationId { get; set; }
    }
}
