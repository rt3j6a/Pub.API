using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddPostRequest {
        [Required]
        public string PostHeader { get; set; } = string.Empty;

        [Required]
        public string PostBody { get; set; } = string.Empty;

        public string? PicturesSourceRoute { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int AccountId { get; set; }
    }

}
