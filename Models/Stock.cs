using System;
using System.Collections.Generic;

namespace BakeryApp.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int ProductId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
