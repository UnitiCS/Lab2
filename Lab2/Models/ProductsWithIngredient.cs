using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class ProductsWithIngredient
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductType { get; set; }

    public string? ProductDescription { get; set; }

    public string? IngredientName { get; set; }

    public string? IngredientType { get; set; }

    public int? IngredientQuantity { get; set; }
}
