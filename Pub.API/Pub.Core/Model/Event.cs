using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class Event
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string? EventDescription { get; set; }

    public DateTime EventAssignedDate { get; set; }

    public DateTime EventPinnedDateTime { get; set; }
}
