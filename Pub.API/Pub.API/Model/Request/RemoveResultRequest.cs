using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class RemoveResultRequest {
        [Required]
        public int ResultId { get; set; }
    }
}
