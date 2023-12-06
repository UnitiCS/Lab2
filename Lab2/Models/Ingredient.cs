using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<BreadRecipe> BreadRecipes { get; set; } = new List<BreadRecipe>();
}
