namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Bakery : Dish
{
    public int Weight { get; private set; }
    public int Calories { get; private set; }
    public bool IsSweet { get; private set; }
    public bool IsFresh { get; private set; }
    public bool IsVegan { get; private set; }

    public Bakery(
        string name,
        double price,
        string description,
        int weight,
        int calories,
        bool isSweet,
        bool isFresh,
        bool isVegan)
        : base(name, price, description, "Bakery")
    {
        ValidateWeight(weight);
        ValidateCalories(calories);
        Weight = weight;
        Calories = calories;
        IsSweet = isSweet;
        IsFresh = isFresh;
        IsVegan = isVegan;
    }

    public override string GetDishType()
    {
        return "Выпечка";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Калории: {Calories}",
            $"Сладкая: {(IsSweet ? "Да" : "Нет")}",
            $"Свежая: {(IsFresh ? "Да" : "Нет")}",
            $"Веганская: {(IsVegan ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}