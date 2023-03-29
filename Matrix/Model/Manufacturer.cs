using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
