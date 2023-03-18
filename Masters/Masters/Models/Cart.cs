using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public int? ProductId { get; set; }

    public string? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual AspNetUser? User { get; set; }
}
