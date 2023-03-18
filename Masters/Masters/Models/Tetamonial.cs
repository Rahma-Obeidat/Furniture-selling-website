using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Tetamonial
{
    public int Id { get; set; }

    public string CommentUser { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual AspNetUser? User { get; set; }
}
