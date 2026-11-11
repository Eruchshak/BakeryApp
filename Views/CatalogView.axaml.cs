using Avalonia.Controls;
using Avalonia.Interactivity;
using BakeryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeryApp.Views;

public partial class CatalogView : UserControl
{
    private List<ProductCard> allCards = new();

    public CatalogView()
    {
        InitializeComponent();
        Loaded += async (s, e) => await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            allCards = await CatalogService.LoadCardsAsync();
            CardsList.ItemsSource = allCards;
        }
        catch (Exception ex)
        {
            MainWindow.Instance.ShowError(ex);
        }
    }

    private void SearchChanged(object? sender, TextChangedEventArgs e)
    {
        string searchText = SearchBox.Text?.ToLower() ?? "";
        var filtered = allCards.Where(c => c.Name.ToLower().Contains(searchText)).ToList();

        CardsList.ItemsSource = filtered;
    }
}