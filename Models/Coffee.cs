using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class Coffee
{
    public int Id { get; set; }

    public string Ctype { get; set; } = null!;

    public string Bean { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int NoOfShots { get; set; }

    public int? Score { get; set; }

    public double? Price { get; set; }
}
