namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Coffee : Dish
{
    public double Volume { get; private set; }
    public bool IsHot { get; private set; }
    public bool HasMilk { get; private set; }
    public int CaffeineLevel { get; private set; }
    public int Calories { get; private set; }

    public Coffee(
        string name,
        double price,
        string description,
        double volume,
        bool isHot,
        bool hasMilk,
        int caffeineLevel,
        int calories)
        : base(name, price, description, "Coffee")
    {
        ValidateVolume(volume);
        ValidateCalories(calories);
        if (caffeineLevel < 0 || caffeineLevel > 10)
        {
            throw new ArgumentException("Уровень кофеина должен быть от 0 до 10.");
        }
        Volume = volume;
        IsHot = isHot;
        HasMilk = hasMilk;
        CaffeineLevel = caffeineLevel;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Кофе";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Горячий: {(IsHot ? "Да" : "Нет")}",
            $"С молоком: {(HasMilk ? "Да" : "Нет")}",
            $"Уровень кофеина: {CaffeineLevel}/10",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}