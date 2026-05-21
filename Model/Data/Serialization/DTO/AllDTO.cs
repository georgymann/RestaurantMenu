namespace Model.Data.Serialization.DTO;
using System.Xml.Serialization;

[XmlInclude(typeof(AppetizerDTO))]
[XmlInclude(typeof(BakeryDTO))]
[XmlInclude(typeof(BreakfastDTO))]
[XmlInclude(typeof(CocktailDTO))]
[XmlInclude(typeof(CoffeeDTO))]
[XmlInclude(typeof(DessertDTO))]
[XmlInclude(typeof(DrinkDTO))]
[XmlInclude(typeof(MainCourseDTO))]
[XmlInclude(typeof(SaladDTO))]
[XmlInclude(typeof(SideDishDTO))]
[XmlInclude(typeof(SoupDTO))]
[XmlInclude(typeof(TeaDTO))]
public class DishDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
}

public class DrinkDTO : DishDTO
{
    public double Volume { get; set; }
    public bool IsCold { get; set; }
    public bool HasSugar { get; set; }
    public int Calories { get; set; }
}

public class TeaDTO : DishDTO
{
    public double Volume { get; set; }
    public bool IsHot { get; set; }
    public bool HasCaffeine { get; set; }
    public int Calories { get; set; }
}

public class CoffeeDTO : DishDTO
{
    public double Volume { get; set; }
    public bool IsHot { get; set; }
    public bool HasMilk { get; set; }
    public int CaffeineLevel { get; set; }
    public int Calories { get; set; }
}

public class CocktailDTO : DishDTO
{
    public double Volume { get; set; }
    public int Calories { get; set; }
    public bool IsAlcoholic { get; set; }
    public bool IsCold { get; set; }
}

public class AppetizerDTO : DishDTO
{
    public int Weight { get; set; }
    public bool IsHot { get; set; }
    public int SpiceLevel { get; set; }
    public bool IsVegan { get; set; }
}

public class BakeryDTO : DishDTO
{
    public int Weight { get; set; }
    public int Calories { get; set; }
    public bool IsSweet { get; set; }
    public bool IsFresh { get; set; }
    public bool IsVegan { get; set; }
}

public class BreakfastDTO : DishDTO
{
    public int Weight { get; set; }
    public int Calories { get; set; }
    public bool IncludesDrink { get; set; }
    public bool IsVegan { get; set; }
}

public class DessertDTO : DishDTO
{
    public int Calories { get; set; }
    public int Weight { get; set; }
    public bool IsFrozen { get; set; }
    public bool IsVegan { get; set; }
}

public class MainCourseDTO : DishDTO
{
    public int Weight { get; set; }
    public int SpiceLevel { get; set; }
    public bool IsVegan { get; set; }
    public int Calories { get; set; }
}

public class SaladDTO : DishDTO
{
    public int Weight { get; set; }
    public bool IsVegan { get; set; }
    public bool HasSeafood { get; set; }
    public int Calories { get; set; }
}

public class SideDishDTO : DishDTO
{
    public int Weight { get; set; }
    public int Calories { get; set; }
    public bool IsVegan { get; set; }
    public bool IsGlutenFree { get; set; }
}

public class SoupDTO : DishDTO
{
    public int Volume { get; set; }
    public bool IsHot { get; set; }
    public bool IsVegan { get; set; }
    public int SpiceLevel { get; set; }
}

public class MenuDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Season { get; set; }
    public List<DishDTO> Dishes { get; set; } = new List<DishDTO>();
}

[XmlInclude(typeof(RestaurantDTO))]
[XmlInclude(typeof(CafeDTO))]
[XmlInclude(typeof(CoffeeShopDTO))]
public class EstablishmentDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public bool IsOpen { get; set; }
    public bool HasSeasonalMenu { get; set; }
    public MenuDTO Menu { get; set; } = new MenuDTO();
    public MenuDTO SeasonalMenu { get; set; } = new MenuDTO();
}

public class CafeDTO : EstablishmentDTO
{
    public bool HasBusinessLunch { get; set; }
    public double AverageCheck { get; set; }
    public bool HasDelivery { get; set; }
    public bool HasOutdoorSeating { get; set; }
}

public class CoffeeShopDTO : EstablishmentDTO
{
    public bool HasAlternativeMilk { get; set; }
    public bool HasBakery { get; set; }
    public double AverageCoffeePrice { get; set; }
    public bool HasWifi { get; set; }
}

public class RestaurantDTO : EstablishmentDTO
{
    public string CuisineType { get; set; }
    public bool HasMichelinStar { get; set; }
    public double AverageCheck { get; set; }
    public bool HasDelivery { get; set; }
}