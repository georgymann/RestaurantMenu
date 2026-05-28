using Avalonia.Controls;
using Model.Core.Abstract;
using Model.Core.Dishes;
using Model.Core.Interfaces;
using RestaurantMenuModel = Model.Core.Menus.Menu;
using System;
using System.Linq;

namespace RestaurantMenu;

public partial class MenuWindow : Window
{
    private Establishment? _establishment;
    private IMenu? _menu;
    private string _saveFormat = "JSON";
    private Action<Establishment, string>? _saveEstablishment;

    public MenuWindow()
    {
        InitializeComponent();
        DishTypeComboBox.ItemsSource = new[]
        {
            "Основное блюдо",
            "Пиво",
            "Напиток",
            "Десерт",
            "Закуска"
        };
        DishTypeComboBox.SelectedIndex = 0;
    }

    public MenuWindow(
        Establishment establishment,
        IMenu menu,
        string saveFormat,
        Action<Establishment, string> saveEstablishment) : this()
    {
        _establishment = establishment;
        _menu = menu;
        _saveFormat = saveFormat;
        _saveEstablishment = saveEstablishment;
        LoadWindowData();
    }

    private void LoadWindowData()
    {
        if (_establishment == null || _menu == null) return;

        WindowTitleText.Text = $"Меню: {_establishment.Name}";
        WindowSubtitleText.Text = $"{_menu} | Формат сохранения: {_saveFormat}";

        CategoryComboBox.ItemsSource = _menu.Dishes
            .Select(dish => dish.Category)
            .Distinct()
            .Prepend("Все категории")
            .ToList();

        CategoryComboBox.SelectedIndex = 0;
        DishesDataGrid.ItemsSource = _menu.Dishes;
    }

    private void FilterDishes(object? sender, SelectionChangedEventArgs e)
    {
        if (_menu == null) return;

        string? category = CategoryComboBox.SelectedItem?.ToString();
        DishesDataGrid.ItemsSource = category == null || category == "Все категории"
            ? _menu.Dishes
            : _menu.Dishes.Where(dish => dish.Category == category).ToArray();
    }

    private void RemoveSelectedDish(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_menu is not RestaurantMenuModel menu) return;
        if (DishesDataGrid.SelectedItem is not Dish selectedDish) return;

        menu.RemoveDish(selectedDish);
        LoadWindowData();
    }

    private void AddDish(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_menu is not RestaurantMenuModel menu) return;

        try
        {
            string name = DishNameTextBox.Text ?? string.Empty;
            string description = DishDescriptionTextBox.Text ?? string.Empty;

            if (!double.TryParse(DishPriceTextBox.Text, out double price))
            {
                WindowSubtitleText.Text = "Цена должна быть числом.";
                return;
            }

            Dish dish = CreateDish(name, price, description);
            menu.AddDish(dish);

            DishNameTextBox.Text = string.Empty;
            DishPriceTextBox.Text = string.Empty;
            DishDescriptionTextBox.Text = string.Empty;
            LoadWindowData();
        }
        catch (Exception ex)
        {
            WindowSubtitleText.Text = ex.Message;
        }
    }

    private void SaveMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_establishment == null || _saveEstablishment == null) return;

        try
        {
            _saveEstablishment(_establishment, _saveFormat);
            WindowSubtitleText.Text = $"Меню сохранено в формате {_saveFormat}.";
        }
        catch (Exception ex)
        {
            WindowSubtitleText.Text = $"Ошибка сохранения: {ex.Message}";
        }
    }

    private Dish CreateDish(string name, double price, string description)
    {
        string type = DishTypeComboBox.SelectedItem?.ToString() ?? "Основное блюдо";
        if (type == "Пиво")
            return new Beer(name, price, description, 0.5, true, false, 210, 4.8, true);
        if (type == "Напиток")
            return new Drink(name, price, description, 0.3, true, false, 120);
        if (type == "Десерт")
            return new Dessert(name, price, description, 300, 150, false, false);
        if (type == "Закуска")
            return new Appetizer(name, price, description, 180, false, 0, false);
        return new MainCourse(name, price, description, 300, 0, false, 500);
    }
}
