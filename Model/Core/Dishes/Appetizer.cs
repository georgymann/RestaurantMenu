namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Appetizer : Dish
{
    public int Weight { get; private set; }
    public bool IsHot { get; private set; }
    public int SpiceLevel { get; private set; }
    public bool IsVegan { get; private set; }

    public Appetizer(
        string name,
        double price,
        string description,
        int weight,
        bool isHot,
        int spiceLevel,
        bool isVegan)
        : base(name, price, description, "Appetizers")
    {
        ValidateWeight(weight);
        ValidateSpiceLevel(spiceLevel);
        Weight = weight;
        IsHot = isHot;
        SpiceLevel = spiceLevel;
        IsVegan = isVegan;
    }
    
    public Appetizer(
        Guid id,
        string name,
        double price,
        string description,
        bool isAvailable,
        int weight,
        bool isHot,
        int spiceLevel,
        bool isVegan)
        : this(name, price, description, weight, isHot, spiceLevel, isVegan)
    {
        _id = id;
        _isAvailable = isAvailable;
    }

    public override string GetDishType()
    {
        return "Закуска";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Горячая: {(IsHot ? "Да" : "Нет")}",
            $"Веганская: {(IsVegan ? "Да" : "Нет")}",
            $"Острота: {SpiceLevel}/10");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}