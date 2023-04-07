using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddTableReservationRequest {
        [Required]
        public string TeamName { get; set; } = string.Empty;

        public string? Comment { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}
