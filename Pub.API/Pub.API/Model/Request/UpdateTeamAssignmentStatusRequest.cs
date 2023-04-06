using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class UpdateTeamAssignmentStatusRequest {
        [Required]
        public int AssignmentId { get; set; }

        [Required]
        public int TeamAssignmentStatusId { get; set; }
    }
}
