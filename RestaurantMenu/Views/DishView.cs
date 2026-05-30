using Avalonia.Media;
using Model.Core.Abstract;
using Model.Core.Dishes;
using System.Collections.Generic;

namespace RestaurantMenu;

public class DishView
{
    private const int DescriptionLimit = 42;

    public Dish OriginalDish { get; }
    public string Name => OriginalDish.Name;
    public string Type => OriginalDish.GetDishType();
    public string Category => GetCategoryTitle(OriginalDish.Category);
    public string Price => $"{OriginalDish.Price:0} ₽";
    public string Description => OriginalDish.Description;
    public string ShortDescription => CutText(OriginalDish.Description, DescriptionLimit);
    public string Portion => GetPortion(OriginalDish);
    public string Calories => GetCalories(OriginalDish);
    public string Parameters => GetParameters(OriginalDish);
    public string Availability => OriginalDish.IsAvailable ? "Да" : "Нет";
    public IBrush AvailabilityBrush => Brush.Parse(OriginalDish.IsAvailable ? "#22C55E" : "#EF4444");

    public DishView(Dish dish)
    {
        OriginalDish = dish;
    }

    private static string CutText(string text, int maxLength)
    {
        if (text.Length <= maxLength)
        {
            return text;
        }

        string cutText = text[..maxLength].TrimEnd();
        int lastSpaceIndex = cutText.LastIndexOf(' ');

        if (lastSpaceIndex > maxLength / 2)
        {
            cutText = cutText[..lastSpaceIndex];
        }

        cutText = cutText.TrimEnd(',', '.', ';', ':', ' ');
        return cutText + "...";
    }

    private static string GetCategoryTitle(string category)
    {
        return category switch
        {
            "Appetizers" => "Закуски",
            "Salads" => "Салаты",
            "Soups" => "Супы",
            "Main Courses" => "Основные блюда",
            "Side Dishes" => "Гарниры",
            "Desserts" => "Десерты",
            "Drinks" => "Напитки",
            "Tea" => "Чай",
            "Coffee" => "Кофе",
            "Beer" => "Пиво",
            "Cocktails" => "Коктейли",
            "Bakery" => "Выпечка",
            "Breakfasts" => "Завтраки",
            _ => category
        };
    }

    private static string GetPortion(Dish dish)
    {
        return dish switch
        {
            Beer beer => $"{beer.Volume:0.##} л",
            Drink drink => $"{drink.Volume:0.##} л",
            Coffee coffee => $"{coffee.Volume:0.##} л",
            Tea tea => $"{tea.Volume:0.##} л",
            Cocktail cocktail => $"{cocktail.Volume:0.##} л",
            Soup soup => $"{soup.Volume} мл",
            MainCourse mainCourse => $"{mainCourse.Weight} г",
            Appetizer appetizer => $"{appetizer.Weight} г",
            Salad salad => $"{salad.Weight} г",
            SideDish sideDish => $"{sideDish.Weight} г",
            Dessert dessert => $"{dessert.Weight} г",
            Bakery bakery => $"{bakery.Weight} г",
            Breakfast breakfast => $"{breakfast.Weight} г",
            _ => "-"
        };
    }

    private static string GetParameters(Dish dish)
    {
        List<string> parameters = new();
        string portion = GetPortion(dish);
        string calories = GetCalories(dish);

        if (portion != "-")
        {
            parameters.Add(portion);
        }

        if (calories != "-")
        {
            parameters.Add(calories);
        }

        return parameters.Count == 0 ? "-" : string.Join(" · ", parameters);
    }

    private static string GetCalories(Dish dish)
    {
        return dish switch
        {
            Beer beer => $"{beer.Calories} ккал",
            Drink drink => $"{drink.Calories} ккал",
            Coffee coffee => $"{coffee.Calories} ккал",
            Tea tea => $"{tea.Calories} ккал",
            Cocktail cocktail => $"{cocktail.Calories} ккал",
            MainCourse mainCourse => $"{mainCourse.Calories} ккал",
            Salad salad => $"{salad.Calories} ккал",
            SideDish sideDish => $"{sideDish.Calories} ккал",
            Dessert dessert => $"{dessert.Calories} ккал",
            Bakery bakery => $"{bakery.Calories} ккал",
            Breakfast breakfast => $"{breakfast.Calories} ккал",
            _ => "-"
        };
    }
}
