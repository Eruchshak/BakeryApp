namespace BakeryApp.Services;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
    public decimal TotalPrice => Price * Quantity;
}