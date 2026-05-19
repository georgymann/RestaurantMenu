namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class Cocktail : Dish
{
    public double Volume { get; private set; }
    public int Calories { get; private set; }
    public bool IsAlcoholic { get; private set; }
    public bool IsCold { get; private set; }


    public Cocktail(
        string name,
        double price,
        string description,
        double volume,
        int calories,
        bool isAlcoholic,
        bool isCold)
        : base(name, price, description, "Cocktails")
    {
        ValidateVolume(volume);
        ValidateCalories(calories);
        Volume = volume;
        Calories = calories;
        IsAlcoholic = isAlcoholic;
        IsCold = isCold;
    }

    public override string GetDishType()
    {
        return "Коктейль";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Объём: {Volume}л",
            $"Калории: {Calories}",
            $"Алкогольный: {(IsAlcoholic ? "Да" : "Нет")}",
            $"Холодный: {(IsCold ? "Да" : "Нет")}");
    }

    public override string ToString()
    {
        return $"{Name} | {Volume}л | {Price}₽";
    }
}