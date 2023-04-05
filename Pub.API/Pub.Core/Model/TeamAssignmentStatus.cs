using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class TeamAssignmentStatus
{
    public int TeamAssignmentStatusId { get; set; }

    public string TeamAssignmentStatusName { get; set; } = null!;

    public int TeamAssignmentStatusValue { get; set; }
}
