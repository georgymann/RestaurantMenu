namespace Model.Core.Establishments;

using Model.Core.Abstract;
using Model.Core.Menus;

public class Restaurant : Establishment
{
    public string CuisineType { get; private set; }
    public bool HasMichelinStar { get; private set; }
    public double AverageCheck { get; private set; }
    public bool HasDelivery { get; private set; }

    public Restaurant(
        string name,
        string address,
        string description,
        Menu menu,
        double rating,
        string cuisineType,
        bool hasMichelinStar,
        double averageCheck,
        bool hasDelivery)
        : base(name, address, description, menu, rating)
    {
        if (averageCheck < 0)
            throw new ArgumentException("Средний чек не может быть отрицательным.");
        if (string.IsNullOrWhiteSpace(cuisineType))
            throw new ArgumentException("Тип кухни не может быть пустым.");
        CuisineType = cuisineType;
        HasMichelinStar = hasMichelinStar;
        AverageCheck = averageCheck;
        HasDelivery = hasDelivery;
    }

    public override string GetEstablishmentType()
    {
        return "Ресторан";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            base.GetInfo(),
            $"Тип кухни: {CuisineType}",
            $"Мишлен: {(HasMichelinStar ? "Да" : "Нет")}",
            $"Средний чек: {AverageCheck}₽",
            $"Доставка: {(HasDelivery ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | Ресторан";
    }
}