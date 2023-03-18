using System;
using System.Collections.Generic;

namespace Masters.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string NameOnCard { get; set; } = null!;

    public int ExpYear { get; set; }

    public int ExpMonth { get; set; }

    public string CardNumber { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public double Ammount { get; set; }
}
