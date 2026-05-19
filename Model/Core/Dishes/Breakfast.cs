namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Breakfast : Dish
{
    public int Weight { get; private set; }
    public int Calories { get; private set; }
    public bool IncludesDrink { get; private set; }
    public bool IsVegan { get; private set; }

    public Breakfast(
        string name,
        double price,
        string description,
        int weight,
        int calories,
        bool includesDrink,
        bool isVegan)
        : base(name, price, description, "Breakfasts")
    {
        ValidateWeight(weight);
        ValidateCalories(calories);
        Weight = weight;
        Calories = calories;
        IncludesDrink = includesDrink;
        IsVegan = isVegan;
    }

    public override string GetDishType()
    {
        return "Завтрак";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Калории: {Calories}",
            $"Напиток включён: {(IncludesDrink ? "Да" : "Нет")}",
            $"Веганский: {(IsVegan ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}