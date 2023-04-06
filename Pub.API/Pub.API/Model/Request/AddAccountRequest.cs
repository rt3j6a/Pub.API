using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddAccountRequest {

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
