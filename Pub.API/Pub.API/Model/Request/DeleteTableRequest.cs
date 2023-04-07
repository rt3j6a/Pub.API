using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class DeleteTableRequest {
        [Required]
        public int TableId { get; set; }
    }

}
