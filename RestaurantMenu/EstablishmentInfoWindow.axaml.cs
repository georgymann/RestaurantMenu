using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Model.Core.Abstract;

namespace RestaurantMenu;

public partial class EstablishmentInfoWindow : Window
{
    public EstablishmentInfoWindow()
    {
        InitializeComponent();
    }

    private void OpenLink(object? sender, PointerPressedEventArgs e)
    {
        LinkOpener.Open(sender);
    }

    public EstablishmentInfoWindow(Establishment establishment) : this()
    {
        EstablishmentView view = new(establishment);

        NameTextBlock.Text = view.Name;
        TypeTextBlock.Text = view.Type;
        StatusTextBlock.Text = view.Status;
        StatusDot.Fill = view.IsOpen ? Brushes.LimeGreen : Brushes.IndianRed;
        RatingTextBlock.Text = view.Rating;
        DescriptionTextBlock.Text = view.Description;
        FieldsList.ItemsSource = view.Fields;
    }
}
