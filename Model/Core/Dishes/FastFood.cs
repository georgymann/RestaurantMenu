namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class FastFood : Dish
{
    public int Weight { get; set; }
    public int Calories { get; set; }
    public int SpiceLevel { get; set; }
    public bool IsVegan { get; set; }
    public bool IsComboAvailable { get; set; }

    public FastFood(
        string name,
        double price,
        string description,
        int weight,
        int calories,
        int spiceLevel,
        bool isVegan,
        bool isComboAvailable)
        : base(name, price, description, "Fast Food")
    {
        Weight = weight;
        Calories = calories;
        SpiceLevel = spiceLevel;
        IsVegan = isVegan;
        IsComboAvailable = isComboAvailable;
    }

    public override string GetDishType()
    {
        return "Фастфуд";
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
            $"Комбо доступно: {(IsComboAvailable ? "Да" : "Нет")}",
            $"Острота: {SpiceLevel}/10");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}