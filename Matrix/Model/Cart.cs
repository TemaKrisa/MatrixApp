using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Cart
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public virtual Product Product { get; set; } = null!;
}
