using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetAllQuestionsForEventRequest {
        [Required]
        public int EventId { get; set; }
    }
}
