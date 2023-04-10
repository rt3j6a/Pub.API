using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetPostRequest {
        [Required]
        public int PostId { get; set; }
    }
}
