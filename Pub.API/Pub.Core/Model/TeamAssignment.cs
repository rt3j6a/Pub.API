using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class TeamAssignment
{
    public long TeamAssignmentId { get; set; }

    public string TeamName { get; set; } = null!;

    public int TeamMemberCount { get; set; }

    public string SourceEmailAddress { get; set; } = null!;

    public int TeamAssignmentStatusId { get; set; }

    public int EventId { get; set; }
}
