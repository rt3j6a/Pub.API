using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddTableRequest {
        [Required]
        public string TableName { get; set; } = string.Empty;
        [Required]
        public decimal MaxSeatNumber { get; set; }
    }
}
