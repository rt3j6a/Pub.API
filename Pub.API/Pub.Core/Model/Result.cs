using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class Result
{
    public long ResultId { get; set; }

    public string TeamName { get; set; } = null!;

    public double TeamScore { get; set; }

    public int EventId { get; set; }
}
