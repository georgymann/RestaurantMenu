namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Tea : Dish
{
    public double Volume { get; set; }
    public bool IsHot { get; set; }
    public bool HasCaffeine { get; set; }
    public int Calories { get; set; }

    public Tea(
        string name,
        double price,
        string description,
        double volume,
        bool isHot,
        bool hasCaffeine,
        int calories)
        : base(name, price, description, "Tea")
    {
        Volume = volume;
        IsHot = isHot;
        HasCaffeine = hasCaffeine;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Чай";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Горячий: {(IsHot ? "Да" : "Нет")}",
            $"С кофеином: {(HasCaffeine ? "Да" : "Нет")}",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}