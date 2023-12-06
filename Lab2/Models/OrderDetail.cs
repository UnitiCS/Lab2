using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string? CustomerName { get; set; }

    public string? ProductName { get; set; }

    public string? ProductType { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Supplier { get; set; }

    public int? SupplyQuantity { get; set; }

    public decimal? SupplyPrice { get; set; }

    public DateTime? SupplyDate { get; set; }
}
