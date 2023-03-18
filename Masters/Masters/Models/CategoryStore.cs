using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class CategoryStore
{
    public int Id { get; set; }

    public int? CatId { get; set; }

    public int? StoreId { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual Store? Store { get; set; }
}
