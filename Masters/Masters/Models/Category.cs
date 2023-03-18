using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public virtual ICollection<CategoryStore> CategoryStores { get; } = new List<CategoryStore>();
}
