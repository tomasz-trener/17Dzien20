using System;
using System.Collections.Generic;

namespace P10DatabaseFrist.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool Premium { get; set; }

    public int? MaterialCategoryId { get; set; }

    public virtual MaterialCategory? MaterialCategory { get; set; }

    public virtual ICollection<ProductProductAdjective> ProductProductAdjectives { get; } = new List<ProductProductAdjective>();
}
