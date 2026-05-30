using Model.Core.Abstract;
using Model.Core.Establishments;
using System.Collections.Generic;
using System.Globalization;

namespace RestaurantMenu;

public class InfoField
{
    public string Label { get; }
    public string Value { get; }
    public string Url { get; }
    public bool IsLink => !string.IsNullOrWhiteSpace(Url);

    public InfoField(string label, string value, string url = "")
    {
        Label = label;
        Value = value;
        Url = url;
    }
}

public class EstablishmentView
{
    private readonly Establishment _establishment;

    public EstablishmentView(Establishment establishment)
    {
        _establishment = establishment;
    }

    public string Name => _establishment.Name;
    public string Type => _establishment.GetEstablishmentType();
    public string Description => _establishment.Description;
    public string Status => _establishment.IsOpen ? "Открыто" : "Закрыто";
    public bool IsOpen => _establishment.IsOpen;
    public string Rating => $"Рейтинг {_establishment.Rating.ToString("0.0", CultureInfo.InvariantCulture)}";

    public List<InfoField> Fields => BuildFields();

    private List<InfoField> BuildFields()
    {
        List<InfoField> fields = new()
        {
            new InfoField("Адрес", _establishment.Address)
        };

        if (!string.IsNullOrWhiteSpace(_establishment.Contacts.Phone))
        {
            fields.Add(new InfoField("Телефон", _establishment.Contacts.Phone));
        }

        if (!string.IsNullOrWhiteSpace(_establishment.Contacts.Website))
        {
            fields.Add(new InfoField("Сайт", _establishment.Contacts.Website, _establishment.Contacts.Website));
        }

        switch (_establishment)
        {
            case Restaurant restaurant:
                fields.Add(new InfoField("Тип кухни", restaurant.CuisineType));
                fields.Add(new InfoField("Звезда Мишлен", restaurant.HasMichelinStar ? "Да" : "Нет"));
                fields.Add(new InfoField("Средний чек", $"{restaurant.AverageCheck:0} ₽"));
                fields.Add(new InfoField("Доставка", restaurant.HasDelivery ? "Да" : "Нет"));
                break;

            case Cafe cafe:
                fields.Add(new InfoField("Бизнес-ланч", cafe.HasBusinessLunch ? "Да" : "Нет"));
                fields.Add(new InfoField("Средний чек", $"{cafe.AverageCheck:0} ₽"));
                fields.Add(new InfoField("Доставка", cafe.HasDelivery ? "Да" : "Нет"));
                fields.Add(new InfoField("Летняя веранда", cafe.HasOutdoorSeating ? "Да" : "Нет"));
                break;

            case CoffeeShop coffeeShop:
                fields.Add(new InfoField("Альт. молоко", coffeeShop.HasAlternativeMilk ? "Да" : "Нет"));
                fields.Add(new InfoField("Своя выпечка", coffeeShop.HasBakery ? "Да" : "Нет"));
                fields.Add(new InfoField("Цена кофе", $"{coffeeShop.AverageCoffeePrice:0} ₽"));
                fields.Add(new InfoField("Wi-Fi", coffeeShop.HasWifi ? "Да" : "Нет"));
                break;
        }

        return fields;
    }
}
