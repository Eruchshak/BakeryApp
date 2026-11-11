using Avalonia.Controls;
using Avalonia.Interactivity;
using BakeryApp.Data;
using BakeryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BakeryApp.Views;

public partial class OrdersView : UserControl
{
    public OrdersView()
    {
        InitializeComponent();
        Loaded += async (s, e) =>
        {
            try
            {
                using var db = Db.Create();
                var orders = await db.Orders.Include(o => o.User).ToListAsync();
                OrdersList.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                ErrorText.Text = "Ошибка загрузки: " + ex.Message;
            }
        };
    }

    private void OpenOrderClick(object? sender, RoutedEventArgs e)
    {
        var order = OrdersList.SelectedItem as Order;

        if (order == null)
        {
            MainWindow.Instance.ShowMessage("Сначала выберите заказ из списка!");
            return;
        }

        MainWindow.Instance.OpenPage(new OrderDetailsView { CurrentOrderId = order.OrderId });
    }
}