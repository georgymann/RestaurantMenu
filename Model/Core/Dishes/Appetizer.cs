namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Appetizer : Dish
{
    public int Weight { get; set; }
    public bool IsHot { get; set; }
    public int SpiceLevel { get; set; }
    public bool IsVegan { get; set; }

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
        Weight = weight;
        IsHot = isHot;
        SpiceLevel = spiceLevel;
        IsVegan = isVegan;
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