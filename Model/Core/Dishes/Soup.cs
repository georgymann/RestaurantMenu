namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Soup : Dish
{
    public int Volume { get; set; }
    public bool IsHot { get; set; }
    public bool IsVegan { get; set; }
    public int SpiceLevel { get; set; }

    public Soup(
        string name,
        double price,
        string description,
        int volume,
        bool isHot,
        bool isVegan,
        int spiceLevel)
        : base(name, price, description, "Soups")
    {
        Volume = volume;
        IsHot = isHot;
        IsVegan = isVegan;
        SpiceLevel = spiceLevel;
    }

    public override string GetDishType()
    {
        return "Суп";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}мл",
            $"Горячий: {(IsHot ? "Да" : "Нет")}",
            $"Веганский: {(IsVegan ? "Да" : "Нет")}",
            $"Острота: {SpiceLevel}/10");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}мл | {Price}₽";
    }
}