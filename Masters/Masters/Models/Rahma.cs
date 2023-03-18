using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Rahma
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }
}
