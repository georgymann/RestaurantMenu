namespace Model.Core.Establishments;

using Model.Core.Abstract;
using Model.Core.Menus;

public class Cafe : Establishment
{
    public bool HasBusinessLunch { get; private set; }
    public double AverageCheck { get; private set; }
    public bool HasDelivery { get; private set; }
    public bool HasOutdoorSeating { get; private set; }

    public Cafe(
        string name,
        string address,
        string description,
        Menu menu,
        double rating,
        bool hasBusinessLunch,
        double averageCheck,
        bool hasDelivery,
        bool hasOutdoorSeating)
        : base(name, address, description, menu, rating)
    {
        if (averageCheck < 0)
            throw new ArgumentException("Средний чек не может быть отрицательным.");
        HasBusinessLunch = hasBusinessLunch;
        AverageCheck = averageCheck;
        HasDelivery = hasDelivery;
        HasOutdoorSeating = hasOutdoorSeating;
    }

    public override string GetEstablishmentType()
    {
        return "Кафе";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            base.GetInfo(),
            $"Бизнес-ланч: {(HasBusinessLunch ? "Да" : "Нет")}",
            $"Средний чек: {AverageCheck}₽",
            $"Доставка: {(HasDelivery ? "Да" : "Нет")}",
            $"Летняя веранда: {(HasOutdoorSeating ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | Кафе";
    }
}