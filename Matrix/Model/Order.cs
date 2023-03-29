using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int Client { get; set; }

    public int DeliveryType { get; set; }

    public int Status { get; set; }

    public decimal TotalSum { get; set; }

    public virtual Account ClientNavigation { get; set; } = null!;

    public virtual DeliveryType DeliveryTypeNavigation { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

    public virtual OrderStatus StatusNavigation { get; set; } = null!;
}
