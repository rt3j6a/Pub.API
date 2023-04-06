using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class LoginRequest {
        [Required]
        public string LoginName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } =  string.Empty;
    }
}
