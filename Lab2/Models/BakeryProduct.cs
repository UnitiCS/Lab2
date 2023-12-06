using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class BakeryProduct
{
    public int ProductId { get; set; }

    public int? SupplyId { get; set; }

    public int? OrderId { get; set; }

    public int? BreadRecipeId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<BreadRecipe> BreadRecipes { get; set; } = new List<BreadRecipe>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
