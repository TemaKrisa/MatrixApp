using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Manufacturer { get; set; }

    public int Brand { get; set; }

    public int Discount { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }

    public string Description { get; set; } = null!;

    public byte[] Image { get; set; } = null!;

    public int Count { get; set; }

    public int Category { get; set; }

    public float? AvgRating { get; set; }

    public virtual Brand BrandNavigation { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ProductCategory CategoryNavigation { get; set; } = null!;

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();
}
