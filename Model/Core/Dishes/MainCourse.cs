namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class MainCourse : Dish
{
    public int Weight { get; set; }
    public int SpiceLevel { get; set; }
    public bool IsVegan { get; set; }
    public int Calories { get; set; }

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