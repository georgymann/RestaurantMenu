namespace Model.Core.Establishments;

using Model.Core.Abstract;
using Model.Core.Menus;

public class Restaurant : Establishment
{
    public string CuisineType { get; set; }
    public bool HasMichelinStar { get; set; }
    public double AverageCheck { get; set; }
    public bool HasDelivery { get; set; }

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