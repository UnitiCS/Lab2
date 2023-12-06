using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class Supply
{
    public int SupplyId { get; set; }

    public int? ProductId { get; set; }

    public string? Supplier { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? SupplyDate { get; set; }

    public virtual BakeryProduct? Product { get; set; }
}
