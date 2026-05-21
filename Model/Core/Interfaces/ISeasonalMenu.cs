using Model.Core.Menus;
namespace Model.Core.Interfaces;

public interface ISeasonalMenu
{
    IMenu SeasonalMenu { get; }
    bool HasSeasonalMenu { get; }
    
    void AddSeasonalMenu(SeasonalMenu menu);
    void RemoveSeasonalMenu();
}