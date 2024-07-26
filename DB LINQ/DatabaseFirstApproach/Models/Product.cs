using System;
using System.Collections.Generic;

namespace DatabaseFirstApproach.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Category { get; set; } = null!;

    public decimal Offer { get; set; }

    public string Quantity { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
