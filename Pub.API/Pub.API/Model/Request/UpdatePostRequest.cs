using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdatePostRequest {
        [Required]
        public int PostId { get; set; }

        public string? PostHeader { get; set; }

        public string? PostBody { get; set;}
    }
}
