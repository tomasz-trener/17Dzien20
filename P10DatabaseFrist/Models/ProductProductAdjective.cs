using System;
using System.Collections.Generic;

namespace P10DatabaseFrist.Models;

public partial class ProductProductAdjective
{
    public int ProductId { get; set; }

    public int ProductAdjectiveId { get; set; }

    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductAdjective ProductAdjective { get; set; } = null!;
}
