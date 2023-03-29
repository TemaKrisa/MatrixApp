using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public int Manufacturer { get; set; }

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
