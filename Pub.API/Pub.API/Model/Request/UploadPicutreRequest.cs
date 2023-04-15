using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UploadPicutreRequest {
        [Required]
        public string PictureDataBase64 { get; set; } = string.Empty;

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public int EventId { get; set; }
    }
}
