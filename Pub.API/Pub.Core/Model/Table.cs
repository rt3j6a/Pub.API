using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class Table
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public int MaxSeatNumber { get; set; }
}
