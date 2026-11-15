using Avalonia.Controls;
using Avalonia.Interactivity;
using BakeryApp.Data;
using BakeryApp.Models;
using BakeryApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BakeryApp.Views;

public partial class CartView : UserControl
{
    public CartView()
    {
        InitializeComponent();
        Loaded += (s, e) => LoadCart();
    }

    private void LoadCart()
    {
        if (Session.CurrentCart == null || Session.CurrentCart.Count == 0)
        {
            MainWindow.Instance.ShowMessage("Корзина пуста.");
            return;
        }

        CartList.ItemsSource = Session.CurrentCart;
        decimal total = Session.CurrentCart.Sum(item => item.TotalPrice);
        TotalText.Text = $"Итого: {total:N2} руб.";
    }

    private async void ConfirmClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            using var db = Db.Create();

            var order = new Order
            {
                UserId = Session.CurrentUser.UserId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                TotalAmount = Session.CurrentCart.Sum(item => item.TotalPrice)
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            foreach (var item in Session.CurrentCart)
            {
                db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }
            await db.SaveChangesAsync();

            Session.CurrentCart.Clear();

            MainWindow.Instance.ShowMessage($"Заказ №{order.OrderId} успешно создан!");
            MainWindow.Instance.OpenPage(new CatalogView());
        }
        catch (Exception ex)
        {
            MainWindow.Instance.ShowError(ex);
        }
    }

    private void CancelClick(object? sender, RoutedEventArgs e)
    {
        Session.CurrentCart?.Clear();
        MainWindow.Instance.ShowMessage("Заказ отменен.");
        MainWindow.Instance.OpenPage(new CatalogView());
    }
}