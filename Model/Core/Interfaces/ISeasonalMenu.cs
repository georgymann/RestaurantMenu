namespace Model.Core.Interfaces;

public interface ISeasonalMenu
{
    IMenu SeasonalMenu { get; }
    bool HasSeasonalMenu { get; }
    
    void AddSeasonalMenu(IMenu menu);
    void RemoveSeasonalMenu();
}