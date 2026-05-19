namespace Model.Core.Establishments;

using Model.Core.Abstract;
using Model.Core.Menus;

public class CoffeeShop : Establishment
{
    public bool HasAlternativeMilk { get; private set; }
    public bool HasBakery { get; private set; }
    public double AverageCoffeePrice { get; private set; }
    public bool HasWifi { get; private set; }

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
        if (averageCoffeePrice < 0)
            throw new ArgumentException("Средняя цена кофе не может быть отрицательной.");
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