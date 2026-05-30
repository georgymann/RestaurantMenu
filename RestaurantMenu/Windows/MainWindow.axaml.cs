using Avalonia.Controls;
using Avalonia.Input;
using Model.Core.Abstract;
using Model.Core.Establishments;
using Model.Core.Interfaces;
using Model.Data.Serialization;
using Model.Data.Serialization.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantMenu;

public partial class MainWindow : Window
{
    private readonly List<Establishment> _establishments = new();
    private readonly JsonDataSerializer _jsonSerializer = new();
    private readonly XmlDataSerializer _xmlSerializer = new();
    private readonly string _dataDirectory = Path.Combine(AppContext.BaseDirectory, "DataFiles");
    private string _currentFormat = "JSON";

    public MainWindow()
    {
        InitializeComponent();
        InitializeControls();
        LoadEstablishments();
    }

    private void LoadEstablishments()
    {
        try
        {
            Directory.CreateDirectory(GetFormatDirectory("JSON"));
            Directory.CreateDirectory(GetFormatDirectory("XML"));

            string[] files = Directory.GetFiles(GetFormatDirectory("JSON"), "*.json");

            foreach (string file in files)
            {
                Establishment? establishment = _jsonSerializer.JSONDeserializer<EstablishmentDTO>(file);
                if (establishment != null)
                {
                    _establishments.Add(establishment);
                }
            }

            if (_establishments.Count == 0)
            {
                EmptyHintText.Text = "Файлы заведений не найдены.";
            }
        }
        catch (Exception ex)
        {
            _establishments.Clear();
            EmptyHintText.Text = $"Ошибка чтения файлов: {ex.Message}";
        }

        EstablishmentComboBox.ItemsSource = _establishments;
    }

    private void SaveEstablishments(string format)
    {
        foreach (Establishment establishment in _establishments)
        {
            SaveEstablishment(establishment, format);
        }
    }

    private void SaveEstablishment(Establishment establishment, string format)
    {
        string directory = GetFormatDirectory(format);
        Directory.CreateDirectory(directory);

        if (format == "XML")
        {
            string filePath = Path.Combine(directory, GetFileName(establishment, "xml"));
            _xmlSerializer.XmlSerializer(establishment, filePath);
        }
        else
        {
            string filePath = Path.Combine(directory, GetFileName(establishment, "json"));
            _jsonSerializer.JSONSerializer(establishment, filePath);
        }
    }

    private string GetFormatDirectory(string format)
    {
        string folderName = format == "XML" ? "Xml" : "Json";
        return Path.Combine(_dataDirectory, folderName);
    }

    private string GetFileName(Establishment establishment, string extension)
    {
        string fileName = $"{establishment.GetType().Name}_{establishment.Name}.{extension}";

        foreach (char invalidChar in Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(invalidChar, '_');
        }

        return fileName;
    }

    private void InitializeControls()
    {
        EstablishmentTypeComboBox.ItemsSource = new string[]
        {
            "Все заведения",
            "Рестораны",
            "Кафе",
            "Кофейни"
        };
        EstablishmentTypeComboBox.SelectedIndex = 0;

        FormatComboBox.ItemsSource = new string[]
        {
            "Не выбрано",
            "JSON",
            "XML"
        };

        FormatComboBox.SelectedIndex = 1;
        MenuTypeComboBox.IsEnabled = false;
        ShowMenuButton.IsEnabled = false;
    }

    private void FilterEstablishments(object? sender, SelectionChangedEventArgs e)
    {
        if (_establishments.Count == 0) return;

        if (EstablishmentTypeComboBox.SelectedIndex == 1)
        {
            EstablishmentComboBox.ItemsSource = _establishments
                .Where(establishment => establishment is Restaurant)
                .ToList();
        }
        else if (EstablishmentTypeComboBox.SelectedIndex == 2)
        {
            EstablishmentComboBox.ItemsSource = _establishments
                .Where(establishment => establishment is Cafe)
                .ToList();
        }
        else if (EstablishmentTypeComboBox.SelectedIndex == 3)
        {
            EstablishmentComboBox.ItemsSource = _establishments
                .Where(establishment => establishment is CoffeeShop)
                .ToList();
        }
        else
        {
            EstablishmentComboBox.ItemsSource = _establishments;
        }

        EstablishmentComboBox.SelectedIndex = -1;
        ShowEmptyState();
        MenuTypeComboBox.IsEnabled = false;
        ShowMenuButton.IsEnabled = false;
    }

    private void ShowEmptyState()
    {
        DetailsPanel.IsVisible = false;
        EmptyStatePanel.IsVisible = true;
    }

    private void SelectEstablishment(object? sender, SelectionChangedEventArgs e)
    {
        Establishment? establishment = EstablishmentComboBox.SelectedItem as Establishment;

        if (establishment == null)
        {
            ShowEmptyState();
            MenuTypeComboBox.IsEnabled = false;
            ShowMenuButton.IsEnabled = false;
            return;
        }

        EstablishmentView view = new(establishment);
        DetailsNameText.Text = view.Name;
        DetailsTypeText.Text = view.Type;
        DetailsStatusText.Text = view.Status;
        DetailsStatusDot.Fill = view.IsOpen
            ? Avalonia.Media.Brushes.LimeGreen
            : Avalonia.Media.Brushes.IndianRed;
        DetailsRatingText.Text = view.Rating;
        DetailsDescriptionText.Text = view.Description;
        DetailsFieldsList.ItemsSource = view.Fields;

        EmptyStatePanel.IsVisible = false;
        DetailsPanel.IsVisible = true;

        List<string> menuTypes = new List<string>();
        menuTypes.Add("Обычное меню");

        if (establishment.HasSeasonalMenu)
        {
            menuTypes.Add("Сезонное меню");
        }

        MenuTypeComboBox.ItemsSource = menuTypes;
        MenuTypeComboBox.SelectedIndex = 0;
        MenuTypeComboBox.IsEnabled = true;
        ShowMenuButton.IsEnabled = true;
    }

    private void ChangeFormat(object? sender, SelectionChangedEventArgs e)
    {
        string? selectedFormat = FormatComboBox.SelectedItem?.ToString();

        if (selectedFormat == null || selectedFormat == "Не выбрано" || selectedFormat == _currentFormat)
        {
            return;
        }

        try
        {
            SaveEstablishments(selectedFormat);
            _currentFormat = selectedFormat;
            ShowStatus($"Данные сохранены в формате {_currentFormat}.");
        }
        catch (Exception ex)
        {
            ShowStatus($"Ошибка сохранения: {ex.Message}");
        }
    }

    private void ShowStatus(string message)
    {
        StatusText.Text = message;
        StatusText.IsVisible = true;
    }

    private void OpenLink(object? sender, PointerPressedEventArgs e)
    {
        LinkOpener.Open(sender);
    }

    private void OpenMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Establishment? establishment = EstablishmentComboBox.SelectedItem as Establishment;
        if (establishment == null) return;

        bool isSeasonalMenu = MenuTypeComboBox.SelectedItem?.ToString() == "Сезонное меню";
        IMenu selectedMenu;
        if (isSeasonalMenu)
        {
            selectedMenu = establishment.SeasonalMenu;
        }
        else
        {
            selectedMenu = establishment.Menu;
        }

        MenuWindow menuWindow = new MenuWindow(establishment, selectedMenu, _currentFormat, SaveEstablishment);
        menuWindow.Show();
    }
}
