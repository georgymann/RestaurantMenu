namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class MainCourse : Dish
{
    public int Weight { get; private set; }
    public int SpiceLevel { get; private set; }
    public bool IsVegan { get; private set; }
    public int Calories { get; private set; }

    public MainCourse(
        string name,
        double price,
        string description,
        int weight,
        int spiceLevel,
        bool isVegan,
        int calories)
        : base(name, price, description, "Main Courses")
    {
        ValidateWeight(weight);
        ValidateSpiceLevel(spiceLevel);
        ValidateCalories(calories);
        Weight = weight;
        SpiceLevel = spiceLevel;
        IsVegan = isVegan;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Основное блюдо";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Вес: {Weight}г",
            $"Калории: {Calories}",
            $"Веганское: {(IsVegan ? "Да" : "Нет")}",
            $"Острота: {SpiceLevel}/10");
    }

    public override string ToString()
    {
        return $"{Name} | {Weight}г | {Price}₽";
    }
}