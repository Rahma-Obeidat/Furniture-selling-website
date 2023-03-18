using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public string ImagePath { get; set; } = null!;

    public int? CategoryStoreId { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual CategoryStore? CategoryStore { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
