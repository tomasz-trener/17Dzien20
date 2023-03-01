using System;
using System.Collections.Generic;

namespace P10DatabaseFrist.Models;

public partial class ProductAdjective
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductProductAdjective> ProductProductAdjectives { get; } = new List<ProductProductAdjective>();
}
