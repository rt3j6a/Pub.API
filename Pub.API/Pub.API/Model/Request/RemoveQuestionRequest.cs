using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class RemoveQuestionRequest {
        [Required]
        public int QuestionId { get; set; }
    }
}
