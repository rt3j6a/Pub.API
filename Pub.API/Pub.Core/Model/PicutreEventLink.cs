using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class PicutreEventLink
{
    public int PictureEventLinkId { get; set; }

    public string? PicturesSourceRoute { get; set; }

    public int EventId { get; set; }
}
