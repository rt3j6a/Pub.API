using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddQuestionRequest {
        [Required]
        public int EventId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
