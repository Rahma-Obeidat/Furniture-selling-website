using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string? StatusPublish { get; set; }

    public virtual ICollection<CategoryStore> CategoryStores { get; } = new List<CategoryStore>();
}
