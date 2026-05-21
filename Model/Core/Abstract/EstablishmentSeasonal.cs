using Model.Core.Interfaces;
using Model.Core.Menus;

namespace Model.Core.Abstract;

public abstract partial class Establishment
{
    protected SeasonalMenu _seasonalMenu;
    protected bool _hasSeasonalMenu;

    public IMenu SeasonalMenu => _seasonalMenu;
    public bool HasSeasonalMenu => _hasSeasonalMenu;

    public void AddSeasonalMenu(SeasonalMenu menu)
    {
        if (menu == null)
        {
            throw new ArgumentNullException(nameof(menu));
        }

        _seasonalMenu = menu;
        _hasSeasonalMenu = true;
    }

    public void RemoveSeasonalMenu()
    {
        _seasonalMenu = new SeasonalMenu();
        _hasSeasonalMenu = false;
    }
}