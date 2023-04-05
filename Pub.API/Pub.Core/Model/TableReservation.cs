using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class TableReservation
{
    public long TableReservationId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? Comment { get; set; }

    public int TableId { get; set; }

    public int EventId { get; set; }
}
