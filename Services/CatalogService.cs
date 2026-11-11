using BakeryApp.Data;
using BakeryApp.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BakeryApp.Services;

public static class CatalogService
{
    public static async Task<List<ProductCard>> LoadCardsAsync()
    {
        using var db = Db.Create();

        // Получаем все товары из базы
        var products = await db.Products.AsNoTracking().ToListAsync();
        var cards = new List<ProductCard>();

        foreach (var p in products)
        {
            cards.Add(new ProductCard
            {
                Id = p.ProductId,
                Name = p.Name,
                PriceText = $"{p.Price:N2} руб.",
                Image = UiSupport.Picture(p.Name + ".png"),
                // Если цена больше 500, делаем фон карточки персиковым, иначе белым
                Background = UiSupport.Brush(p.Price > 500 ? "SurfaceBrush" : "NormalBrush")
            });
        }
        return cards;
    }
}