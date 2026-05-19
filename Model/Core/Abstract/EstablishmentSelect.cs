namespace Model.Core.Abstract;

public abstract partial class Establishment
{
    public Dish[] Select(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        List<Dish> selectedDishes = new List<Dish>();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.GetType() == type)
            {
                selectedDishes.Add(dish);
            }
        }
        return selectedDishes.ToArray();
    }

    public Dish[] Select(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Категория не может быть пустой.");
        }

        List<Dish> selectedDishes = new List<Dish>();
        foreach (Dish dish in Menu.Dishes)
        {
            if (dish.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            {
                selectedDishes.Add(dish);
            }
        }
        return selectedDishes.ToArray();
    }
}