using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class GetTeamAssignmentRequest {
        [Required]
        public int AssignmentId { get; set; }
    }
}
