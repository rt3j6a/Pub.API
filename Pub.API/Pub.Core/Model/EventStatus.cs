using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class EventStatus
{
    public int EventStatusId { get; set; }

    public string EventStatusName { get; set; } = null!;

    public int EventStatusValue { get; set; }
}
