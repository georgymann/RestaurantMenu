using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Model.Core.Abstract;

namespace RestaurantMenu;

public partial class EditDishWindow : Window
{
    private readonly Dish? _dish;

    public EditDishWindow()
    {
        InitializeComponent();
    }

    public EditDishWindow(Dish dish) : this()
    {
        _dish = dish;

        DishView view = new(dish);
        DishNameText.Text = view.Name;
        DishTypeText.Text = view.Type;
        ReadonlyNameText.Text = view.Name;
        ReadonlyCategoryText.Text = view.Category;
        PortionText.Text = view.Portion;
        CaloriesText.Text = view.Calories;
        DescriptionBox.Text = dish.Description;
        PriceBox.Text = dish.Price.ToString("0.##", CultureInfo.InvariantCulture);
        AvailableCheck.IsChecked = dish.IsAvailable;
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }

    private void Save(object? sender, RoutedEventArgs e)
    {
        if (_dish == null)
        {
            Close(false);
            return;
        }

        string description = DescriptionBox.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(description))
        {
            ShowError("Описание не может быть пустым.");
            return;
        }

        string priceText = (PriceBox.Text ?? string.Empty).Trim().Replace(',', '.');
        if (!double.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out double price) || price < 0)
        {
            ShowError("Введите корректную цену (неотрицательное число).");
            return;
        }

        try
        {
            _dish.ChangeDescription(description);
            _dish.ChangePrice(price);

            if (AvailableCheck.IsChecked == true)
            {
                _dish.Enable();
            }
            else
            {
                _dish.Disable();
            }

            Close(true);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.IsVisible = true;
    }
}
