using Avalonia.Controls;
using Avalonia.Interactivity;
using BakeryApp.Data;
using BakeryApp.Models;
using BakeryApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BakeryApp.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        Loaded += async (s, e) => await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            using var db = Db.Create();
            var users = await db.Users.ToListAsync();
            UsersBox.ItemsSource = users;
            if (users.Count > 0) UsersBox.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MainWindow.Instance.ShowError(ex);
        }
    }

    private void LoginClick(object? sender, RoutedEventArgs e)
    {
        // Берем выбранного пользователя из списка (в единственном числе)
        User? user = UsersBox.SelectedItem as User;

        if (user == null)
        {
            MainWindow.Instance.ShowMessage("Выберите пользователя из списка!");
            return;
        }

        // Проверяем пароль
        if (PasswordBox.Text != user.Password)
        {
            MainWindow.Instance.ShowError(new Exception("Неверный пароль! Попробуйте снова."));
            return;
        }

        MainWindow.Instance.ShowMessage($"Вы вошли как: {user.FullName}");
        Session.CurrentUser = user;
        MainWindow.Instance.OpenPage(new CatalogView());
    }
}