using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetResultRequest {
        [Required]
        public int ResultId { get; set; }
    }
}
