using Model.Core.Abstract;
namespace Model.Core.Menus;

public partial class Menu
{
    public static Menu operator +(Menu menu, Dish dish)
    {
        if (menu == null)
        {
            throw new ArgumentNullException(nameof(menu));
        }

        menu.AddDish(dish);
        return menu;
    }
    
    public void AddDish(Dish dish)
    {
        if (dish == null)
        {
            throw new ArgumentNullException(nameof(dish));
        }

        _dishes.Add(dish);
    }
    
    public void AddDish(Dish[] dishes)
    {
        if (dishes == null)
        {
            throw new ArgumentNullException(nameof(dishes));
        }

        foreach (Dish dish in dishes)
        {
            AddDish(dish);
        }
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

    public void RemoveDish(Guid id)
    {
        foreach (Dish dish in _dishes)
        {
            if (dish.ID == id)
            {
                RemoveDish(dish);
                return;
            }
        }
        throw new InvalidOperationException("Блюдо с таким ID не найдено.");
    }
    
    public Dish[] FindDishes(Predicate<Dish> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }
        
        List<Dish> foundDishes = new List<Dish>();
        foreach (Dish dish in _dishes)
        {
            if (predicate(dish))
            {
                foundDishes.Add(dish);
            }
        }
        return foundDishes.ToArray();
    }
}