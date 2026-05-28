namespace Model.Core.Dishes;

public class Beer : Drink
{
    public double AlcoholPercent { get; private set; }
    public bool IsDraft { get; private set; }

    public Beer(
        string name,
        double price,
        string description,
        double volume,
        bool isCold,
        bool hasSugar,
        int calories,
        double alcoholPercent,
        bool isDraft)
        : base(name, price, description, "Beer", volume, isCold, hasSugar, calories)
    {
        if (alcoholPercent < 0)
        {
            throw new ArgumentException("Процент алкоголя не может быть отрицательным.");
        }

        AlcoholPercent = alcoholPercent;
        IsDraft = isDraft;
    }

    public Beer(
        Guid id,
        string name,
        double price,
        string description,
        bool isAvailable,
        double volume,
        bool isCold,
        bool hasSugar,
        int calories,
        double alcoholPercent,
        bool isDraft)
        : this(name, price, description, volume, isCold, hasSugar, calories, alcoholPercent, isDraft)
    {
        _id = id;
        _isAvailable = isAvailable;
    }

    public override string GetDishType()
    {
        return "Пиво";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Алкоголь: {AlcoholPercent}%",
            $"Разливное: {(IsDraft ? "Да" : "Нет")}",
            $"Холодное: {(IsCold ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {AlcoholPercent}% | {Price}₽";
    }
}
