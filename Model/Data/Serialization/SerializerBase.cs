using Model.Core.Abstract;
using Model.Core.Contacts;
using Model.Core.Dishes;
using Model.Core.Establishments;
using Model.Core.Menus;
using Model.Data.Serialization.DTO;


namespace Model.Data.Serialization;

public abstract class SerializerBase
{
    protected DishDTO SerializeDish(Dish dish)
    {
        DishDTO dto = dish switch
        {
            Beer beer => new BeerDTO
            {
                Kind = "Beer",
                Volume = beer.Volume,
                IsCold = beer.IsCold,
                HasSugar = beer.HasSugar,
                Calories = beer.Calories,
                AlcoholPercent = beer.AlcoholPercent,
                IsDraft = beer.IsDraft
            },
            Drink drink => new DrinkDTO
            {
                Kind = "Drink",
                Volume = drink.Volume,
                IsCold = drink.IsCold,
                HasSugar = drink.HasSugar,
                Calories = drink.Calories
            },
            Tea tea => new TeaDTO
            {
                Kind = "Tea",
                Volume = tea.Volume,
                IsHot = tea.IsHot,
                HasCaffeine = tea.HasCaffeine,
                Calories = tea.Calories
            },
            Coffee coffee => new CoffeeDTO
            {
                Kind = "Coffee",
                Volume = coffee.Volume,
                IsHot = coffee.IsHot,
                HasMilk = coffee.HasMilk,
                CaffeineLevel = coffee.CaffeineLevel,
                Calories = coffee.Calories
            },
            Cocktail cocktail => new CocktailDTO
            {
                Kind = "Cocktail",
                Volume = cocktail.Volume,
                IsCold = cocktail.IsCold,
                Calories = cocktail.Calories,
                IsAlcoholic = cocktail.IsAlcoholic
            },
            Appetizer appetizer => new AppetizerDTO
            {
                Kind = "Appetizer",
                Weight = appetizer.Weight,
                IsHot = appetizer.IsHot,
                SpiceLevel = appetizer.SpiceLevel,
                IsVegan = appetizer.IsVegan
            },
            Bakery bakery => new BakeryDTO
            {
                Kind = "Bakery",
                Weight = bakery.Weight,
                Calories = bakery.Calories,
                IsSweet = bakery.IsSweet,
                IsFresh = bakery.IsFresh,
                IsVegan = bakery.IsVegan
            },
            Breakfast breakfast => new BreakfastDTO
            {
                Kind = "Breakfast",
                Weight = breakfast.Weight,
                Calories = breakfast.Calories,
                IncludesDrink = breakfast.IncludesDrink,
                IsVegan = breakfast.IsVegan
            },
            Dessert dessert => new DessertDTO
            {
                Kind = "Dessert",
                Calories = dessert.Calories,
                Weight = dessert.Weight,
                IsFrozen = dessert.IsFrozen,
                IsVegan = dessert.IsVegan
            },
            MainCourse mainCourse => new MainCourseDTO
            {
                Kind = "MainCourse",
                Weight = mainCourse.Weight,
                SpiceLevel = mainCourse.SpiceLevel,
                IsVegan = mainCourse.IsVegan,
                Calories = mainCourse.Calories
            },
            Salad salad => new SaladDTO
            {
                Kind = "Salad",
                Weight = salad.Weight,
                IsVegan = salad.IsVegan,
                HasSeafood = salad.HasSeafood,
                Calories = salad.Calories
            },
            SideDish sideDish => new SideDishDTO
            {
                Kind = "SideDish",
                Weight = sideDish.Weight,
                Calories = sideDish.Calories,
                IsVegan = sideDish.IsVegan,
                IsGlutenFree = sideDish.IsGlutenFree
            },
            Soup soup => new SoupDTO
            {
                Kind = "Soup",
                Volume = soup.Volume,
                IsHot = soup.IsHot,
                IsVegan = soup.IsVegan,
                SpiceLevel = soup.SpiceLevel
            },
            _ => throw new NotSupportedException($"Неизвестный тип блюда: {dish.GetType().Name}.")
        };

        dto.Id = dish.ID;
        dto.Name = dish.Name;
        dto.Price = dish.Price;
        dto.Description = dish.Description;
        dto.Category = dish.Category;
        dto.IsAvailable = dish.IsAvailable;
        return dto;
    }

    protected MenuDTO SerializeMenu(Menu menu)
    {
        string season = menu is SeasonalMenu seasonalMenu ? seasonalMenu.Season : string.Empty;

        return new MenuDTO
        {
            Name = menu.Name,
            Description = menu.Description,
            Season = season,
            Dishes = menu.Dishes.Select(SerializeDish).ToList()
        };
    }

    protected ContactsDTO SerializeContacts(ContactInfo contacts)
    {
        return new ContactsDTO
        {
            Phone = contacts.Phone,
            Website = contacts.Website
        };
    }

    protected ContactInfo DeserializeContacts(ContactsDTO? dto)
    {
        ContactInfo contacts = new();

        if (dto == null)
        {
            return contacts;
        }

        if (!string.IsNullOrWhiteSpace(dto.Phone))
        {
            contacts.ChangePhone(dto.Phone);
        }

        if (!string.IsNullOrWhiteSpace(dto.Website))
        {
            contacts.ChangeWebsite(dto.Website);
        }

        return contacts;
    }

    protected EstablishmentDTO SerializeEstablishment(Establishment establishment)
    {
        MenuDTO menuDto = SerializeMenu(establishment.Menu);
        MenuDTO seasonalMenuDto = establishment.HasSeasonalMenu
            ? SerializeMenu((Menu)establishment.SeasonalMenu)
            : new MenuDTO();

        EstablishmentDTO dto = establishment switch
        {
            Cafe cafe => new CafeDTO
            {
                Kind = "Cafe",
                HasBusinessLunch = cafe.HasBusinessLunch,
                AverageCheck = cafe.AverageCheck,
                HasDelivery = cafe.HasDelivery,
                HasOutdoorSeating = cafe.HasOutdoorSeating
            },
            CoffeeShop coffeeShop => new CoffeeShopDTO
            {
                Kind = "CoffeeShop",
                HasAlternativeMilk = coffeeShop.HasAlternativeMilk,
                HasBakery = coffeeShop.HasBakery,
                AverageCoffeePrice = coffeeShop.AverageCoffeePrice,
                HasWifi = coffeeShop.HasWifi
            },
            Restaurant restaurant => new RestaurantDTO
            {
                Kind = "Restaurant",
                CuisineType = restaurant.CuisineType,
                HasMichelinStar = restaurant.HasMichelinStar,
                AverageCheck = restaurant.AverageCheck,
                HasDelivery = restaurant.HasDelivery
            },
            _ => throw new NotSupportedException($"Неизвестный тип заведения: {establishment.GetType().Name}.")
        };

        dto.Id = establishment.ID;
        dto.Name = establishment.Name;
        dto.Address = establishment.Address;
        dto.Description = establishment.Description;
        dto.Rating = establishment.Rating;
        dto.IsOpen = establishment.IsOpen;
        dto.HasSeasonalMenu = establishment.HasSeasonalMenu;
        dto.Menu = menuDto;
        dto.SeasonalMenu = seasonalMenuDto;
        dto.Contacts = SerializeContacts(establishment.Contacts);
        return dto;
    }

    protected Dish DeserializeDish(DishDTO dto)
    {
        return dto switch
        {
            BeerDTO d => new Beer(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.IsCold, d.HasSugar, d.Calories, d.AlcoholPercent, d.IsDraft),
            DrinkDTO d => new Drink(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.IsCold, d.HasSugar, d.Calories),
            TeaDTO d => new Tea(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.IsHot, d.HasCaffeine, d.Calories),
            CoffeeDTO d => new Coffee(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.IsHot, d.HasMilk, d.CaffeineLevel, d.Calories),
            CocktailDTO d => new Cocktail(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.Calories, d.IsAlcoholic, d.IsCold),
            AppetizerDTO d => new Appetizer(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.IsHot, d.SpiceLevel, d.IsVegan),
            BakeryDTO d => new Bakery(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.Calories, d.IsSweet, d.IsFresh, d.IsVegan),
            BreakfastDTO d => new Breakfast(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.Calories, d.IncludesDrink, d.IsVegan),
            DessertDTO d => new Dessert(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Calories, d.Weight, d.IsFrozen, d.IsVegan),
            MainCourseDTO d => new MainCourse(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.SpiceLevel, d.IsVegan, d.Calories),
            SaladDTO d => new Salad(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.IsVegan, d.HasSeafood, d.Calories),
            SideDishDTO d => new SideDish(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Weight, d.Calories, d.IsVegan, d.IsGlutenFree),
            SoupDTO d => new Soup(d.Id, d.Name, d.Price, d.Description, d.IsAvailable,
                d.Volume, d.IsHot, d.IsVegan, d.SpiceLevel),
            _ => throw new NotSupportedException($"Неизвестный тип DTO блюда: {dto.GetType().Name}.")
        };
    }

    protected Menu DeserializeMenu(MenuDTO dto)
    {
        List<Dish> dishes = dto.Dishes.Select(DeserializeDish).ToList();

        if (!string.IsNullOrWhiteSpace(dto.Season))
        {
            SeasonalMenu seasonalMenu = new(dto.Name, dto.Description, dto.Season);
            foreach (Dish dish in dishes) { seasonalMenu.AddDish(dish); }
            return seasonalMenu;
        }

        Menu menu = new(dto.Name, dto.Description);
        foreach (Dish dish in dishes) { menu.AddDish(dish); }
        return menu;
    }

    protected Establishment DeserializeEstablishment(EstablishmentDTO dto)
    {
        Menu menu = DeserializeMenu(dto.Menu);
        SeasonalMenu? seasonalMenu = dto.HasSeasonalMenu
            ? (SeasonalMenu)DeserializeMenu(dto.SeasonalMenu)
            : null;

        Establishment establishment = dto switch
        {
            RestaurantDTO d => new Restaurant(d.Id, d.Name, d.Address, d.Description, d.IsOpen,
                menu, d.Rating, d.CuisineType, d.HasMichelinStar, d.AverageCheck, d.HasDelivery),
            CafeDTO d => new Cafe(d.Id, d.Name, d.Address, d.Description, d.IsOpen,
                menu, d.Rating, d.HasBusinessLunch, d.AverageCheck, d.HasDelivery, d.HasOutdoorSeating),
            CoffeeShopDTO d => new CoffeeShop(d.Id, d.Name, d.Address, d.Description, d.IsOpen,
                menu, d.Rating, d.HasAlternativeMilk, d.HasBakery, d.AverageCoffeePrice, d.HasWifi),
            _ => throw new NotSupportedException($"Неизвестный тип DTO заведения: {dto.GetType().Name}.")
        };

        establishment.ChangeContacts(DeserializeContacts(dto.Contacts));
        if (seasonalMenu != null) { establishment.AddSeasonalMenu(seasonalMenu); }
        return establishment;
    }
}
