using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdateQuestionRequest {
        [Required]
        public int QuestionId { get; set; }
        [Required]

        public string Content { get; set; } = string.Empty;
    }
}
