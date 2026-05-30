using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Model.Core.Abstract;
using Model.Core.Dishes;
using Model.Core.Establishments;
using Model.Core.Interfaces;
using RestaurantMenuModel = Model.Core.Menus.Menu;

namespace RestaurantMenu;

public partial class AddDishWindow : Window
{
    private static readonly (string Key, string Label)[] AllTypes =
    {
        ("Appetizer", "Закуска"),
        ("Salad", "Салат"),
        ("Soup", "Суп"),
        ("MainCourse", "Основное блюдо"),
        ("SideDish", "Гарнир"),
        ("Dessert", "Десерт"),
        ("Drink", "Напиток"),
        ("Cocktail", "Коктейль"),
        ("Beer", "Пиво"),
        ("Coffee", "Кофе"),
        ("Tea", "Чай"),
        ("Bakery", "Выпечка"),
        ("Breakfast", "Завтрак"),
    };

    private readonly RestaurantMenuModel? _menu;
    private string[] _typeKeys = Array.Empty<string>();

    public AddDishWindow()
    {
        InitializeComponent();
    }

    public AddDishWindow(Establishment establishment, IMenu menu) : this()
    {
        _menu = menu as RestaurantMenuModel;
        EstablishmentText.Text = establishment.Name;

        _typeKeys = AllowedTypeKeys(establishment);
        TypeComboBox.ItemsSource = _typeKeys
            .Select(key => AllTypes.First(type => type.Key == key).Label)
            .ToArray();
        TypeComboBox.SelectedIndex = 0;
    }
    
    private static string[] AllowedTypeKeys(Establishment establishment)
    {
        return establishment switch
        {
            Restaurant => new[]
                { "Appetizer", "Salad", "Soup", "MainCourse", "SideDish", "Dessert", "Drink", "Cocktail", "Beer" },
            Cafe => new[]
                { "Salad", "Soup", "MainCourse", "Dessert", "Drink", "Coffee", "Tea", "Bakery", "Breakfast" },
            CoffeeShop => new[]
                { "Coffee", "Tea", "Bakery", "Dessert", "Breakfast", "Drink" },
            _ => AllTypes.Select(type => type.Key).ToArray()
        };
    }

    private void ChangeType(object? sender, SelectionChangedEventArgs e)
    {
        string? key = SelectedKey();
        if (key != null)
        {
            ShowFieldsFor(key);
        }
    }

    private string? SelectedKey()
    {
        int index = TypeComboBox.SelectedIndex;
        return index >= 0 && index < _typeKeys.Length ? _typeKeys[index] : null;
    }
    
    private void ShowFieldsFor(string key)
    {
        Control[] allFields =
        {
            WeightPanel, VolumePanel, VolumeMlPanel, CaloriesPanel, SpicePanel, CaffeinePanel, AlcoholPanel,
            IsHotCheck, IsColdCheck, IsVeganCheck, HasSugarCheck, HasMilkCheck, HasCaffeineCheck, HasSeafoodCheck,
            IsGlutenFreeCheck, IsFrozenCheck, IsAlcoholicCheck, IsDraftCheck, IsSweetCheck, IsFreshCheck,
            IncludesDrinkCheck
        };

        foreach (Control field in allFields)
        {
            field.IsVisible = false;
        }

        foreach (Control field in FieldsFor(key))
        {
            field.IsVisible = true;
        }
    }

    private Control[] FieldsFor(string key)
    {
        return key switch
        {
            "Appetizer" => new Control[] { WeightPanel, SpicePanel, IsHotCheck, IsVeganCheck },
            "Salad" => new Control[] { WeightPanel, CaloriesPanel, IsVeganCheck, HasSeafoodCheck },
            "Soup" => new Control[] { VolumeMlPanel, SpicePanel, IsHotCheck, IsVeganCheck },
            "MainCourse" => new Control[] { WeightPanel, SpicePanel, CaloriesPanel, IsVeganCheck },
            "SideDish" => new Control[] { WeightPanel, CaloriesPanel, IsVeganCheck, IsGlutenFreeCheck },
            "Dessert" => new Control[] { CaloriesPanel, WeightPanel, IsFrozenCheck, IsVeganCheck },
            "Drink" => new Control[] { VolumePanel, CaloriesPanel, IsColdCheck, HasSugarCheck },
            "Coffee" => new Control[] { VolumePanel, CaffeinePanel, CaloriesPanel, IsHotCheck, HasMilkCheck },
            "Tea" => new Control[] { VolumePanel, CaloriesPanel, IsHotCheck, HasCaffeineCheck },
            "Cocktail" => new Control[] { VolumePanel, CaloriesPanel, IsAlcoholicCheck, IsColdCheck },
            "Beer" => new Control[] { VolumePanel, CaloriesPanel, AlcoholPanel, IsColdCheck, HasSugarCheck, IsDraftCheck },
            "Bakery" => new Control[] { WeightPanel, CaloriesPanel, IsSweetCheck, IsFreshCheck, IsVeganCheck },
            "Breakfast" => new Control[] { WeightPanel, CaloriesPanel, IncludesDrinkCheck, IsVeganCheck },
            _ => Array.Empty<Control>()
        };
    }

    // Собирает объект блюда. Валидацию полей выполняют конструкторы модели.
    private Dish BuildDish(string key, string name, double price, string description)
    {
        bool hot = IsHotCheck.IsChecked == true;
        bool cold = IsColdCheck.IsChecked == true;
        bool vegan = IsVeganCheck.IsChecked == true;

        return key switch
        {
            "Appetizer" => new Appetizer(name, price, description,
                ReadInt(WeightBox, "Вес"), hot, ReadInt(SpiceBox, "Острота"), vegan),
            "Salad" => new Salad(name, price, description,
                ReadInt(WeightBox, "Вес"), vegan, HasSeafoodCheck.IsChecked == true, ReadInt(CaloriesBox, "Калории")),
            "Soup" => new Soup(name, price, description,
                ReadInt(VolumeMlBox, "Объём"), hot, vegan, ReadInt(SpiceBox, "Острота")),
            "MainCourse" => new MainCourse(name, price, description,
                ReadInt(WeightBox, "Вес"), ReadInt(SpiceBox, "Острота"), vegan, ReadInt(CaloriesBox, "Калории")),
            "SideDish" => new SideDish(name, price, description,
                ReadInt(WeightBox, "Вес"), ReadInt(CaloriesBox, "Калории"), vegan, IsGlutenFreeCheck.IsChecked == true),
            "Dessert" => new Dessert(name, price, description,
                ReadInt(CaloriesBox, "Калории"), ReadInt(WeightBox, "Вес"), IsFrozenCheck.IsChecked == true, vegan),
            "Drink" => new Drink(name, price, description,
                ReadDouble(VolumeBox, "Объём"), cold, HasSugarCheck.IsChecked == true, ReadInt(CaloriesBox, "Калории")),
            "Coffee" => new Coffee(name, price, description,
                ReadDouble(VolumeBox, "Объём"), hot, HasMilkCheck.IsChecked == true,
                ReadInt(CaffeineBox, "Кофеин"), ReadInt(CaloriesBox, "Калории")),
            "Tea" => new Tea(name, price, description,
                ReadDouble(VolumeBox, "Объём"), hot, HasCaffeineCheck.IsChecked == true, ReadInt(CaloriesBox, "Калории")),
            "Cocktail" => new Cocktail(name, price, description,
                ReadDouble(VolumeBox, "Объём"), ReadInt(CaloriesBox, "Калории"), IsAlcoholicCheck.IsChecked == true, cold),
            "Beer" => new Beer(name, price, description,
                ReadDouble(VolumeBox, "Объём"), cold, HasSugarCheck.IsChecked == true,
                ReadInt(CaloriesBox, "Калории"), ReadDouble(AlcoholBox, "Алкоголь"), IsDraftCheck.IsChecked == true),
            "Bakery" => new Bakery(name, price, description,
                ReadInt(WeightBox, "Вес"), ReadInt(CaloriesBox, "Калории"),
                IsSweetCheck.IsChecked == true, IsFreshCheck.IsChecked == true, vegan),
            "Breakfast" => new Breakfast(name, price, description,
                ReadInt(WeightBox, "Вес"), ReadInt(CaloriesBox, "Калории"), IncludesDrinkCheck.IsChecked == true, vegan),
            _ => throw new ArgumentException("Неизвестный тип блюда.")
        };
    }

    private void Save(object? sender, RoutedEventArgs e)
    {
        if (_menu == null)
        {
            Close(false);
            return;
        }

        string? key = SelectedKey();
        if (key == null)
        {
            ShowError("Выберите тип блюда.");
            return;
        }

        string name = NameBox.Text?.Trim() ?? string.Empty;
        string description = DescriptionBox.Text?.Trim() ?? string.Empty;

        try
        {
            double price = ReadDouble(PriceBox, "Цена");
            Dish dish = BuildDish(key, name, price, description);
            _menu.AddDish(dish);
            Close(true);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }

    private static int ReadInt(TextBox box, string fieldName)
    {
        string text = (box.Text ?? string.Empty).Trim();
        if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
        {
            throw new ArgumentException($"Поле «{fieldName}» должно быть целым числом.");
        }

        return value;
    }

    private static double ReadDouble(TextBox box, string fieldName)
    {
        string text = (box.Text ?? string.Empty).Trim().Replace(',', '.');
        if (!double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
        {
            throw new ArgumentException($"Поле «{fieldName}» должно быть числом.");
        }

        return value;
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.IsVisible = true;
    }
}
