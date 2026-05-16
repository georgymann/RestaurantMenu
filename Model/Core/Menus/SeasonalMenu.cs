namespace Model.Core.Menus;

public class SeasonalMenu : Menu
{
    protected string _season;

    public string Season => _season;

    public SeasonalMenu() : base()
    {
        _season = "Сезон не указан";
    }

    public SeasonalMenu(string name, string description, string season) : base(name, description)
    {
        if (string.IsNullOrWhiteSpace(season))
        {
            throw new ArgumentException("Сезон не может быть пустым");
        }

        _season = season;
    }

    public override string GetInfo()
    {
        return $"{Name} | {Description} | Сезон: {Season} | Количество блюд: {Count}";
    }
}