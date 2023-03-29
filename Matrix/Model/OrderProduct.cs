using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class OrderProduct
{
    public int Order { get; set; }

    public int Product { get; set; }

    public int Count { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual Product ProductNavigation { get; set; } = null!;
}
