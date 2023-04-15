using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class TestFtpRequest {
        [Required]
        public string data { get; set; }
    }
}
