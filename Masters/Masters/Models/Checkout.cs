using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Checkout
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
