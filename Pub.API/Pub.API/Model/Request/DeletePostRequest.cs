using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class DeletePostRequest {
        [Required]
        public int PostId { get; set; }
    }
}
