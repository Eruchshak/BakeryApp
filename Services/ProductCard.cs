using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace BakeryApp.Services;

public class ProductCard
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string PriceText { get; set; } = "";
    public Bitmap? Image { get; set; }
    public IBrush? Background { get; set; }
}