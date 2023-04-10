using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddResultRequest {
        [Required]
        public string TeamName { get; set; } = string.Empty;
        [Required]
        public float TeamScore { get; set; }

        [Required]
        public int EventId { get; set; }

    }
}
