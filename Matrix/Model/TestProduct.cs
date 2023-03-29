using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class TestProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public double ProductPrice { get; set; }

    public double ProductDiscount { get; set; }
}
