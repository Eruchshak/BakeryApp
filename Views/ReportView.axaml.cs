using Avalonia.Controls;
using BakeryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BakeryApp.Views; // <--- ВОТ ЭТО ОЧЕНЬ ВАЖНО

public partial class ReportView : UserControl
{
    public ReportView()
    {
        InitializeComponent();
        Loaded += async (s, e) =>
        {
            try
            {
                using var db = Db.Create();
                var total = await db.Orders.SumAsync(o => o.TotalAmount);
                TotalSalesText.Text = $"{total:N2} руб.";
            }
            catch (Exception ex)
            {
                MainWindow.Instance.ShowError(ex);
            }
        };
    }
}