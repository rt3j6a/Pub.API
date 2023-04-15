using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class DownloadPictureRequest {
        [Required]
        public int EventId { get; set; }
        [Required]
        public string FileName { get; set; } = string.Empty;
    }
}
