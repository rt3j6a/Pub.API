using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class EventLikedQuestion
{
    public int EventLikedQuestionId { get; set; }

    public string? QuestionContent { get; set; }

    public int? EventId { get; set; }
}
