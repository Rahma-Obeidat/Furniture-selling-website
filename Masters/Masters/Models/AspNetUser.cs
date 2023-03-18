using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string RoleId { get; set; } = null!;

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; } = new List<AspNetUserToken>();

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; } = new List<Checkout>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Tetamonial> Tetamonials { get; } = new List<Tetamonial>();

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();
}
