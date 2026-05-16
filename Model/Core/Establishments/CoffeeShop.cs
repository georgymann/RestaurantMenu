namespace Model.Core.Establishments;

using Model.Core.Abstract;
using Model.Core.Menus;

public class CoffeeShop : Establishment
{
    public bool HasAlternativeMilk { get; set; }
    public bool HasBakery { get; set; }
    public double AverageCoffeePrice { get; set; }
    public bool HasWifi { get; set; }

    public CoffeeShop(
        string name,
        string address,
        string description,
        Menu menu,
        double rating,
        bool hasAlternativeMilk,
        bool hasBakery,
        double averageCoffeePrice,
        bool hasWifi)
        : base(name, address, description, menu, rating)
    {
        HasAlternativeMilk = hasAlternativeMilk;
        HasBakery = hasBakery;
        AverageCoffeePrice = averageCoffeePrice;
        HasWifi = hasWifi;
    }

    public override string GetEstablishmentType()
    {
        return "Кофейня";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            base.GetInfo(),
            $"Альтернативное молоко: {(HasAlternativeMilk ? "Да" : "Нет")}",
            $"Своя выпечка: {(HasBakery ? "Да" : "Нет")}",
            $"Средняя цена кофе: {AverageCoffeePrice}₽",
            $"Wi-Fi: {(HasWifi ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | Кофейня";
    }
}