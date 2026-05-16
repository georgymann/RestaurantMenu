namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Coffee : Dish
{
    public double Volume { get; set; }
    public bool IsHot { get; set; }
    public bool HasMilk { get; set; }
    public int CaffeineLevel { get; set; }
    public int Calories { get; set; }

    public Coffee(
        string name,
        double price,
        string description,
        double volume,
        bool isHot,
        bool hasMilk,
        int caffeineLevel,
        int calories)
        : base(name, price, description, "Coffee")
    {
        Volume = volume;
        IsHot = isHot;
        HasMilk = hasMilk;
        CaffeineLevel = caffeineLevel;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Кофе";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Горячий: {(IsHot ? "Да" : "Нет")}",
            $"С молоком: {(HasMilk ? "Да" : "Нет")}",
            $"Уровень кофеина: {CaffeineLevel}/10",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}