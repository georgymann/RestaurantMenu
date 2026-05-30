using Avalonia.Controls;
using Model.Core.Abstract;
using Model.Core.Interfaces;
using RestaurantMenuModel = Model.Core.Menus.Menu;
using System;
using System.Linq;

namespace RestaurantMenu;

public partial class MenuWindow : Window
{
    private const string MainMenuTitle = "Основное меню";
    private const string SeasonalMenuTitle = "Сезонное меню";

    private Establishment? _establishment;
    private IMenu? _menu;
    private string _saveFormat = "JSON";
    private Action<Establishment, string>? _saveEstablishment;
    private bool _isLoadingMenuTypes;

    public MenuWindow()
    {
        InitializeComponent();
        LoadAvailabilityFilter();
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

        WindowTitleText.Text = "Блюда";
        WindowSubtitleText.Text = "Просмотр и управление блюдами меню";
        SidebarEstablishmentTextBox.Text = _establishment.Name;

        LoadMenuTypes();
        LoadCurrentMenuData();
    }

    private void LoadMenuTypes()
    {
        if (_establishment == null) return;

        _isLoadingMenuTypes = true;

        MenuTypeComboBox.ItemsSource = _establishment.HasSeasonalMenu
            ? new[] { MainMenuTitle, SeasonalMenuTitle }
            : new[] { MainMenuTitle };

        MenuTypeComboBox.SelectedIndex = _menu == _establishment.SeasonalMenu && _establishment.HasSeasonalMenu
            ? 1
            : 0;

        _isLoadingMenuTypes = false;
    }

    private void LoadCurrentMenuData()
    {
        if (_menu == null) return;

        DishView[] dishes = GetDishViews(_menu.Dishes);

        CategoryComboBox.ItemsSource = dishes
            .Select(dish => dish.Category)
            .Distinct()
            .Prepend("Все категории")
            .ToList();
        CategoryComboBox.SelectedIndex = 0;

        ApplyFilters();
    }

    private void LoadAvailabilityFilter()
    {
        AvailabilityComboBox.ItemsSource = new[]
        {
            "Все доступные",
            "В наличии",
            "Нет в наличии"
        };
        AvailabilityComboBox.SelectedIndex = 0;
    }

    private void ChangeMenuType(object? sender, SelectionChangedEventArgs e)
    {
        if (_isLoadingMenuTypes || _establishment == null) return;

        string selectedMenuType = MenuTypeComboBox.SelectedItem?.ToString() ?? MainMenuTitle;
        _menu = selectedMenuType == SeasonalMenuTitle && _establishment.HasSeasonalMenu
            ? _establishment.SeasonalMenu
            : _establishment.Menu;

        LoadCurrentMenuData();
    }

    private void FilterDishes(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void SearchDishes(object? sender, TextChangedEventArgs e)
    {
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        if (_menu == null) return;

        DishView[] dishes = GetDishViews(_menu.Dishes);
        string searchText = SearchTextBox.Text?.Trim() ?? string.Empty;
        string? selectedCategory = CategoryComboBox.SelectedItem?.ToString();
        string? selectedAvailability = AvailabilityComboBox.SelectedItem?.ToString();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            dishes = dishes.Where(dish =>
                dish.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        if (!string.IsNullOrWhiteSpace(selectedCategory) && selectedCategory != "Все категории")
        {
            dishes = dishes.Where(dish => dish.Category == selectedCategory).ToArray();
        }

        if (selectedAvailability == "В наличии")
        {
            dishes = dishes.Where(dish => dish.OriginalDish.IsAvailable).ToArray();
        }
        else if (selectedAvailability == "Нет в наличии")
        {
            dishes = dishes.Where(dish => !dish.OriginalDish.IsAvailable).ToArray();
        }

        DishesListBox.ItemsSource = dishes;
        TotalTextBlock.Text = dishes.Length == _menu.Dishes.Length
            ? $"Всего блюд: {_menu.Dishes.Length}"
            : $"Найдено: {dishes.Length} из {_menu.Dishes.Length}";
    }

    private static DishView[] GetDishViews(Dish[] dishes)
    {
        return dishes.Select(dish => new DishView(dish)).ToArray();
    }

    private void RemoveSelectedDish(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_menu is not RestaurantMenuModel menu) return;
        if (DishesListBox.SelectedItem is not DishView selectedDish) return;

        menu.RemoveDish(selectedDish.OriginalDish);
        LoadCurrentMenuData();
    }

    private async void OpenAddDishWindow(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_establishment == null || _menu == null) return;

        AddDishWindow editor = new(_establishment, _menu);
        bool added = await editor.ShowDialog<bool>(this);

        if (added)
        {
            LoadCurrentMenuData();
            StatusTextBlock.Text = "Блюдо добавлено.";
        }
    }

    private async void EditDish(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not MenuItem { DataContext: DishView dish }) return;

        EditDishWindow editor = new(dish.OriginalDish);
        bool saved = await editor.ShowDialog<bool>(this);

        if (saved)
        {
            LoadCurrentMenuData();
            StatusTextBlock.Text = $"Блюдо «{dish.Name}» обновлено.";
        }
    }

    private void DeleteDish(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not MenuItem { DataContext: DishView dish }) return;
        if (_menu is not RestaurantMenuModel menu) return;

        menu.RemoveDish(dish.OriginalDish);
        LoadCurrentMenuData();
        StatusTextBlock.Text = $"Блюдо «{dish.Name}» удалено.";
    }

    private void OpenEstablishmentInfo(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_establishment == null) return;

        EstablishmentInfoWindow infoWindow = new(_establishment);
        SizeInfoWindowToScreen(infoWindow);
        infoWindow.Show();
    }

    private void SizeInfoWindowToScreen(Window window)
    {
        Avalonia.Platform.Screen? screen = Screens.ScreenFromVisual(this) ?? Screens.Primary;
        if (screen == null) return;

        double scaling = screen.Scaling;
        double screenWidth = screen.WorkingArea.Width / scaling;
        double screenHeight = screen.WorkingArea.Height / scaling;
        
        window.Width = Math.Clamp(screenWidth * 0.38, 460, 720);
        window.MaxHeight = screenHeight * 0.92;
    }

    private void SaveMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_establishment == null || _saveEstablishment == null) return;

        try
        {
            _saveEstablishment(_establishment, _saveFormat);
            StatusTextBlock.Text = $"Меню сохранено в формате {_saveFormat}.";
        }
        catch (Exception ex)
        {
            StatusTextBlock.Text = $"Ошибка сохранения: {ex.Message}";
        }
    }
}
