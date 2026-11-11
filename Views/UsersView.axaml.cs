using Avalonia.Controls;
using BakeryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BakeryApp.Views;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();
        Loaded += async (s, e) =>
        {
            try
            {
                using var db = Db.Create();
                UsersList.ItemsSource = await db.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                MainWindow.Instance.ShowError(ex);
            }
        };
    }
}