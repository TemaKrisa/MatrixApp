using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class DeliveryType
{
    public int TypeiD { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
