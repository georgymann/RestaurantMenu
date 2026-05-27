namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Tea : Dish
{
    public double Volume { get; private set; }
    public bool IsHot { get; private set; }
    public bool HasCaffeine { get; private set; }
    public int Calories { get; private set; }

    public Tea(
        string name,
        double price,
        string description,
        double volume,
        bool isHot,
        bool hasCaffeine,
        int calories)
        : base(name, price, description, "Tea")
    {
        ValidateVolume(volume);
        ValidateCalories(calories);
        Volume = volume;
        IsHot = isHot;
        HasCaffeine = hasCaffeine;
        Calories = calories;
    }
    
    public Tea(
        Guid id,
        string name,
        double price,
        string description,
        bool isAvailable,
        double volume,
        bool isHot,
        bool hasCaffeine,
        int calories)
        : this(name, price, description, volume, isHot, hasCaffeine, calories)
    {
        _id = id;
        _isAvailable = isAvailable;
    }

    public override string GetDishType()
    {
        return "Чай";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Горячий: {(IsHot ? "Да" : "Нет")}",
            $"С кофеином: {(HasCaffeine ? "Да" : "Нет")}",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}