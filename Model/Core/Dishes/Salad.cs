namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Salad : Dish
{
    public int Weight { get; set; }
    public bool IsVegan { get; set; }
    public bool HasSeafood { get; set; }
    public int Calories { get; set; }

    public Salad(
        string name,
        double price,
        string description,
        int weight,
        bool isVegan,
        bool hasSeafood,
        int calories)
        : base(name, price, description, "Salads")
    {
        Weight = weight;
        IsVegan = isVegan;
        HasSeafood = hasSeafood;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Салат";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Калории: {Calories}",
            $"Веганский: {(IsVegan ? "Да" : "Нет")}",
            $"С морепродуктами: {(HasSeafood ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}