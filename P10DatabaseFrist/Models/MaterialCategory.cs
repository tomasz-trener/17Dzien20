using System;
using System.Collections.Generic;

namespace P10DatabaseFrist.Models;

public partial class MaterialCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
