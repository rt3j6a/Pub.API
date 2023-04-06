using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddAccountRequest {

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
