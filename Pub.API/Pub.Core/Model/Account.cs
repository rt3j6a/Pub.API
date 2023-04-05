using System;
using System.Collections.Generic;

namespace Pub.Core.Model;

public partial class Account
{
    public int AccountId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string AccountPasswd { get; set; } = null!;
}
