using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace BakeryApp.Views;

public partial class MainWindow : Window
{
    public static MainWindow Instance { get; private set; } = null!;

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
        PageHost.Content = new LoginView();
    }

    public void OpenPage(UserControl page)
    {
        PageHost.Content = page;


        if (Services.Session.CurrentUser != null)
        {
            UserText.Text = Services.Session.CurrentUser.FullName;

            OrdersButton.IsVisible = Services.Session.IsStaff;
            UsersButton.IsVisible = Services.Session.IsAdmin;
            ReportButton.IsVisible = Services.Session.IsAdmin;
            CartButton.IsVisible = true;
        }
        else
        {
            UserText.Text = "Гость";
            OrdersButton.IsVisible = false;
            UsersButton.IsVisible = false;
            ReportButton.IsVisible = false;
            CartButton.IsVisible = false;
        }
    }

    public void ShowError(Exception error) => MessageText.Text = UiSupport.ErrorText(error);
    public void ShowMessage(string text) => MessageText.Text = text;

    private void CatalogClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new CatalogView());
    }

    private void OrdersClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new OrdersView());
    }

    private void LogoutClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new LoginView());
        ShowMessage("Вы вышли из системы.");
    }

    private void UsersClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new UsersView());
    }

    private void ReportClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new ReportView());
    }
    private void CartClick(object? sender, RoutedEventArgs e)
    {
        OpenPage(new CartView());
    }
}