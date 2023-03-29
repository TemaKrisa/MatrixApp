using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Usertype
{
    public int UsertypeId { get; set; }

    public string UsertypeName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
