namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Dessert : Dish
{
    public int Calories { get; private set; }
    public int Weight { get; private set; }
    public bool IsFrozen { get; private set; }
    public bool IsVegan { get; private set; }

    public Dessert(
        string name,
        double price,
        string description,
        int calories,
        int weight,
        bool isFrozen,
        bool isVegan)
        : base(name, price, description, "Desserts")
    {
        ValidateCalories(calories);
        ValidateWeight(weight);
        Calories = calories;
        Weight = weight;
        IsFrozen = isFrozen;
        IsVegan = isVegan;
    }

    public override string GetDishType()
    {
        return "Десерт";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Калории: {Calories}",
            $"Замороженный: {(IsFrozen ? "Да" : "Нет")}",
            $"Веганский: {(IsVegan ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}