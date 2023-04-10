using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdateResultRequest {
        [Required]
        public int ResultId { get; set; }
        [Required]
        public float TeamScore { get; set; }
    }
}
