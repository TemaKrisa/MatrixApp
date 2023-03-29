using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class Account
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Usertype { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Midname { get; set; }

    public decimal Phone { get; set; }

    public string? Street { get; set; }

    public string? House { get; set; }

    public string? Flat { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Usertype UsertypeNavigation { get; set; } = null!;
}
