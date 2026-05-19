namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class SideDish : Dish
{
    public int Weight { get; private set; }
    public int Calories { get; private set; }
    public bool IsVegan { get; private set; }
    public bool IsGlutenFree { get; private set; }
    
    public SideDish(
        string name,
        double price,
        string description,
        int weight,
        int calories,
        bool isVegan,
        bool isGlutenFree)
        : base(name, price, description, "Side Dishes")
    {
        ValidateWeight(weight);
        ValidateCalories(calories);
        Weight = weight;
        Calories = calories;
        IsVegan = isVegan;
        IsGlutenFree = isGlutenFree;
    }

    public override string GetDishType()
    {
        return "Гарнир";
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
            $"Без глютена: {(IsGlutenFree ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}