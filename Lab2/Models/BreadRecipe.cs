﻿using System;
using System.Collections.Generic;

namespace Lab2.Models;

public partial class BreadRecipe
{
    public int BreadRecipeId { get; set; }

    public int? ProductId { get; set; }

    public int? IngredientId { get; set; }

    public string? ProductName { get; set; }

    public string? IngredientName { get; set; }

    public int? QuantityPerUnit { get; set; }

    public decimal? Price { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual BakeryProduct? Product { get; set; }
}
