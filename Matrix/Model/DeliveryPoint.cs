using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class DeliveryPoint
{
    public int PointId { get; set; }

    public string Street { get; set; } = null!;

    public string House { get; set; } = null!;
}
