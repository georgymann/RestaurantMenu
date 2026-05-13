using Model.Core.Abstract;
namespace Model.Core.Menus;

public partial class Menu
{
    public void AddDish(Dish dish)
    {
        if (dish == null)
        {
            throw new ArgumentNullException(nameof(dish));
        }

        _dishes.Add(dish);
    }

    public void RemoveDish(Dish dish)
    {
        if (dish == null)
        {
            throw new ArgumentNullException(nameof(dish));
        }

        if (!_dishes.Remove(dish))
        {
            throw new InvalidOperationException("Такого блюда нет в меню.");
        }
    }
}