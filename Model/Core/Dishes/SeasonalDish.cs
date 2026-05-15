namespace Model.Core.Dishes;

using Model.Core.Abstract;

public class SeasonalDish : Dish
{
    public string Season { get; set; }
    public DateTime AvailableUntil { get; set; }
    public bool IsLimitedEdition { get; set; }
    public int Calories { get; set; }

    public SeasonalDish(
        string name,
        double price,
        string description,
        string season,
        DateTime availableUntil,
        bool isLimitedEdition,
        int calories)
        : base(name, price, description, "Seasonal Dishes")
    {
        Season = season;
        AvailableUntil = availableUntil;
        IsLimitedEdition = isLimitedEdition;
        Calories = calories;
    }

    public override string GetDishType()
    {
        return "Сезонное блюдо";
    }

    public override string GetInfo()
    {
        return string.Join(
            Environment.NewLine,
            $"Название: {Name}",
            $"Цена: {Price}₽",
            $"Сезон: {Season}",
            $"Доступно до: {AvailableUntil:d}",
            $"Лимитированное: {(IsLimitedEdition ? "Да" : "Нет")}",
            $"Калории: {Calories}");
    }

    public override string ToString()
    {
        return $"{Name} | {Price}₽";
    }
}