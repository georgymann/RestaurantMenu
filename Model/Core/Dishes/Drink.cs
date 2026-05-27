namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Drink : Dish
{
    public double Volume { get; private set; }
    public bool IsCold { get; private set; }
    public bool HasSugar { get; private set; }
    public int Calories { get; private set; }

    public Drink(
        string name,
        double price,
        string description,
        double volume,
        bool isCold,
        bool hasSugar,
        int calories)
        : base(name, price, description, "Drinks")
    {
        ValidateVolume(volume);
        ValidateCalories(calories);
        Volume = volume;
        IsCold = isCold;
        HasSugar = hasSugar;
        Calories = calories;
    }
    
    public Drink(
        Guid id,
        string name,
        double price,
        string description,
        bool isAvailable,
        double volume,
        bool isCold,
        bool hasSugar,
        int calories)
        : this(name, price, description, volume, isCold, hasSugar, calories)
    {
        _id = id;
        _isAvailable = isAvailable;
    }
    
    public override string GetDishType()
    {
        return "Напиток";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Холодный: {(IsCold ? "Да" : "Нет")}",
            $"С сахаром: {(HasSugar ? "Да" : "Нет")}",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}