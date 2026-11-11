using Avalonia.Controls;
using Avalonia.Interactivity;
using BakeryApp.Data;
using BakeryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BakeryApp.Views;

public partial class OrderDetailsView : UserControl
{
    public int CurrentOrderId { get; set; }

    public OrderDetailsView()
    {
        InitializeComponent();
        Loaded += async (s, e) => await LoadOrderAsync();
    }

    private async Task LoadOrderAsync()
    {
        try
        {
            using var db = Db.Create();

            var order = await db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == CurrentOrderId);

            if (order != null)
            {
                HeaderText.Text = $"Заказ № {order.OrderId}";
                ClientText.Text = $"Клиент: {order.User?.FullName ?? "Неизвестен"}";
                DateText.Text = $"Дата: {order.OrderDate:dd.MM.yyyy}";
                TotalText.Text = $"Итого: {order.TotalAmount:N2} руб.";

                ItemsList.ItemsSource = order.OrderItems.ToList();
            }
        }
        catch (Exception ex)
        {
            MainWindow.Instance.ShowError(ex);
        }
    }

    private void BackClick(object? sender, RoutedEventArgs e)
    {
        MainWindow.Instance.OpenPage(new OrdersView());
    }
}