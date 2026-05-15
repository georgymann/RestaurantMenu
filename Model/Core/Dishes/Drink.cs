namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Drink : Dish
{
    public double Volume { get; set; }
    public bool IsCold { get; set; }
    public bool HasSugar { get; set; }
    public int Calories { get; set; }

    public Drink(
        string name,
        double price,
        string description,
        double volume,
        bool isCold,
        bool hasSugar,
        int calories)
        : base(name, price, description, "Drinks")
    {
        Volume = volume;
        IsCold = isCold;
        HasSugar = hasSugar;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Напиток";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Холодный: {(IsCold ? "Да" : "Нет")}",
            $"С сахаром: {(HasSugar ? "Да" : "Нет")}",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}