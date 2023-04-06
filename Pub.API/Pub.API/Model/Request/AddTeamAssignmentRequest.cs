using System.ComponentModel.DataAnnotations;

namespace Pub.API.Model.Request {
    public class AddTeamAssignmentRequest {

        [Required]
        public string TeamName { get; set; } = string.Empty;

        [Required]
        public decimal TeamMemberCount { get; set; }

        [Required]
        [EmailAddress]
        public string SourceEmailAddress { get; set; } = string.Empty;

        [Required]
        public int EventId { get; set; }
    }
}
