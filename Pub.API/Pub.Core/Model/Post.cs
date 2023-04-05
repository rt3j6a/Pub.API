using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class Post
{
    public long PostId { get; set; }

    public string? PostHeader { get; set; }

    public string? PostBody { get; set; }

    public string? PicturesSourceRoute { get; set; }

    public int? EventId { get; set; }

    public int? AccountId { get; set; }
}
