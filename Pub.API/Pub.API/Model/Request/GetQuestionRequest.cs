using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetQuestionRequest {
        [Required]
        public int QuestionId { get; set; }
    }
}
