using Model.Core.Abstract;
using Model.Core.Dishes;
using Model.Core.Establishments;
using Model.Core.Menus;
using Model.Core.Interfaces;
using Model.Data.Serialization.DTO;


namespace Model.Data.Serialization;

public abstract class SerializerBase
{
    protected DishDTO SerializeDish(Dish dish)
    {
        if (dish is Drink drink)
        {
            return new DrinkDTO
            {
                Id = drink.ID,
                Name = drink.Name,
                Price = drink.Price,
                Description = drink.Description,
                Category = drink.Category,
                IsAvailable = drink.IsAvailable,

                Volume = drink.Volume,
                IsCold = drink.IsCold,
                HasSugar = drink.HasSugar,
                Calories = drink.Calories
            };
        }
        
        if (dish is Tea tea)
        {
            return new TeaDTO
            {
                Id = tea.ID,
                Name = tea.Name,
                Price = tea.Price,
                Description = tea.Description,
                Category = tea.Category,
                IsAvailable = tea.IsAvailable,

                Volume = tea.Volume,
                IsHot = tea.IsHot,
                HasCaffeine =  tea.HasCaffeine,
                Calories = tea.Calories
            };
        }
        
        if (dish is Coffee coffee)
        {
            return new CoffeeDTO
            {
                Id = coffee.ID,
                Name = coffee.Name,
                Price = coffee.Price,
                Description = coffee.Description,
                Category = coffee.Category,
                IsAvailable = coffee.IsAvailable,

                Volume = coffee.Volume,
                IsHot = coffee.IsHot,
                HasMilk =  coffee.HasMilk,
                CaffeineLevel = coffee.CaffeineLevel,
                Calories = coffee.Calories
            };
        }

        if (dish is Cocktail cocktail)
        {
            return new CocktailDTO
            {
                Id = cocktail.ID,
                Name = cocktail.Name,
                Price = cocktail.Price,
                Description = cocktail.Description,
                Category = cocktail.Category,
                IsAvailable = cocktail.IsAvailable,

                Volume = cocktail.Volume,
                IsCold = cocktail.IsCold,
                Calories = cocktail.Calories,
                IsAlcoholic = cocktail.IsAlcoholic
            };
        }
        
        if (dish is Appetizer appetizer)
        {
            return new AppetizerDTO
            {
                Id = appetizer.ID,
                Name = appetizer.Name,
                Price = appetizer.Price,
                Description = appetizer.Description,
                Category = appetizer.Category,
                IsAvailable = appetizer.IsAvailable,

                Weight = appetizer.Weight,
                IsHot = appetizer.IsHot,
                SpiceLevel = appetizer.SpiceLevel,
                IsVegan = appetizer.IsVegan
            };
        }
        
        if (dish is Bakery bakery)
        {
            return new BakeryDTO
            {
                Id = bakery.ID,
                Name = bakery.Name,
                Price = bakery.Price,
                Description = bakery.Description,
                Category = bakery.Category,
                IsAvailable = bakery.IsAvailable,

                Weight = bakery.Weight,
                Calories = bakery.Calories,
                IsSweet =  bakery.IsSweet,
                IsFresh =  bakery.IsFresh,
                IsVegan = bakery.IsVegan
            };
        }
        
        if (dish is Breakfast breakfast)
        {
            return new BreakfastDTO
            {
                Id = breakfast.ID,
                Name = breakfast.Name,
                Price = breakfast.Price,
                Description = breakfast.Description,
                Category = breakfast.Category,
                IsAvailable = breakfast.IsAvailable,

                Weight = breakfast.Weight,
                Calories = breakfast.Calories,
                IncludesDrink = breakfast.IncludesDrink,
                IsVegan = breakfast.IsVegan
            };
        }

        if (dish is Dessert dessert)
        {
            return new DessertDTO
            {
                Id = dessert.ID,
                Name = dessert.Name,
                Price = dessert.Price,
                Description = dessert.Description,
                Category = dessert.Category,
                IsAvailable = dessert.IsAvailable,

                Calories = dessert.Calories,
                Weight = dessert.Weight,
                IsFrozen = dessert.IsFrozen,
                IsVegan = dessert.IsVegan
            };
        }
        
        if (dish is MainCourse mainCourse)
        {
            return new MainCourseDTO
            {
                Id = mainCourse.ID,
                Name = mainCourse.Name,
                Price = mainCourse.Price,
                Description = mainCourse.Description,
                Category = mainCourse.Category,
                IsAvailable = mainCourse.IsAvailable,

                Weight = mainCourse.Weight,
                SpiceLevel = mainCourse.SpiceLevel,
                IsVegan = mainCourse.IsVegan,
                Calories = mainCourse.Calories
            };
        }
        
        if (dish is Salad salad)
        {
            return new SaladDTO
            {
                Id = salad.ID,
                Name = salad.Name,
                Price = salad.Price,
                Description = salad.Description,
                Category = salad.Category,
                IsAvailable = salad.IsAvailable,

                Weight = salad.Weight,
                IsVegan = salad.IsVegan,
                HasSeafood = salad.HasSeafood,
                Calories = salad.Calories
            };
        }
        
        if (dish is SideDish sideDish)
        {
            return new SideDishDTO
            {
                Id = sideDish.ID,
                Name = sideDish.Name,
                Price = sideDish.Price,
                Description = sideDish.Description,
                Category = sideDish.Category,
                IsAvailable = sideDish.IsAvailable,

                Weight = sideDish.Weight,
                Calories = sideDish.Calories,
                IsVegan = sideDish.IsVegan,
                IsGlutenFree = sideDish.IsGlutenFree
            };
        }

        if (dish is Soup soup)
        {
            return new SoupDTO
            {
                Id = soup.ID,
                Name = soup.Name,
                Price = soup.Price,
                Description = soup.Description,
                Category = soup.Category,
                IsAvailable = soup.IsAvailable,

                Volume = soup.Volume,
                IsHot = soup.IsHot,
                IsVegan = soup.IsVegan,
                SpiceLevel = soup.SpiceLevel
            };
        }

        throw new Exception($"Unknown dish type");
    }

    protected MenuDTO SerializeMenu(Menu menu)
    {
        string season = string.Empty;

        if (menu is SeasonalMenu seasonalMenu) { season = seasonalMenu.Season; }
        
        return new MenuDTO
        {
            Name = menu.Name,
            Description = menu.Description,  
            Season = season,
            Dishes = menu.Dishes.Select(SerializeDish).ToList()
        };
    }

    protected EstablishmentDTO SerializeEstablishment(Establishment establishment)
    {
        MenuDTO menuDto = SerializeMenu(establishment.Menu);
        MenuDTO seasonalMenuDto;
        if (establishment.HasSeasonalMenu) { seasonalMenuDto = SerializeMenu((Menu)establishment.SeasonalMenu); }
        else { seasonalMenuDto = new MenuDTO(); }
        
        if (establishment is Cafe cafe)
        {
            return new CafeDTO
            {
                Id = cafe.ID,
                Name = cafe.Name,
                Address = cafe.Address,
                Description = cafe.Description,
                Rating = cafe.Rating,
                IsOpen = cafe.IsOpen,
                HasSeasonalMenu = cafe.HasSeasonalMenu,
                Menu = menuDto,
                SeasonalMenu = seasonalMenuDto,

                HasBusinessLunch = cafe.HasBusinessLunch,
                AverageCheck = cafe.AverageCheck,
                HasDelivery = cafe.HasDelivery,
                HasOutdoorSeating = cafe.HasOutdoorSeating
            };
        }
        
        if (establishment is CoffeeShop coffeeShop)
        {
            return new CoffeeShopDTO
            {
                Id = coffeeShop.ID,
                Name = coffeeShop.Name,
                Address = coffeeShop.Address,
                Description = coffeeShop.Description,
                Rating = coffeeShop.Rating,
                IsOpen = coffeeShop.IsOpen,
                HasSeasonalMenu = coffeeShop.HasSeasonalMenu,
                Menu = menuDto,
                SeasonalMenu = seasonalMenuDto,

                HasAlternativeMilk = coffeeShop.HasAlternativeMilk,
                HasBakery = coffeeShop.HasBakery,
                AverageCoffeePrice = coffeeShop.AverageCoffeePrice,
                HasWifi = coffeeShop.HasWifi
            };
        }

        if (establishment is Restaurant restaurant)
        {
            return new RestaurantDTO
            {
                Id = restaurant.ID,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Description = restaurant.Description,
                Rating = restaurant.Rating,
                IsOpen = restaurant.IsOpen,
                HasSeasonalMenu = restaurant.HasSeasonalMenu,
                Menu = menuDto,
                SeasonalMenu = seasonalMenuDto,
                
                CuisineType = restaurant.CuisineType,
                HasMichelinStar = restaurant.HasMichelinStar,
                AverageCheck = restaurant.AverageCheck,
                HasDelivery = restaurant.HasDelivery
            };
        }

        throw new Exception($"Unknown establishment type");
    }

    protected Dish DeserializeDish(DishDTO dto)
    {
        if (dto is DrinkDTO drinkDTO)
        {
            return new Drink(
                drinkDTO.Id,
                drinkDTO.Name, 
                drinkDTO.Price,
                drinkDTO.Description, 
                drinkDTO.IsAvailable,
                drinkDTO.Volume, 
                drinkDTO.IsCold, 
                drinkDTO.HasSugar, 
                drinkDTO.Calories
                );
        }

        if (dto is TeaDTO teaDTO)
        {
            return new Tea( 
                teaDTO.Id,
                teaDTO.Name,
                teaDTO.Price,
                teaDTO.Description,
                teaDTO.IsAvailable,
                teaDTO.Volume,
                teaDTO.IsHot,
                teaDTO.HasCaffeine,
                teaDTO.Calories
                );
        }

        if (dto is CoffeeDTO coffeeDTO)
        {
            return new Coffee(
                coffeeDTO.Id,
                coffeeDTO.Name,
                coffeeDTO.Price,
                coffeeDTO.Description,
                coffeeDTO.IsAvailable,
                coffeeDTO.Volume,
                coffeeDTO.IsHot,
                coffeeDTO.HasMilk,
                coffeeDTO.CaffeineLevel,
                coffeeDTO.Calories
                );
        }

        if (dto is CocktailDTO cocktailDTO)
        {
            return new Cocktail(
                cocktailDTO.Id,
                cocktailDTO.Name,
                cocktailDTO.Price,
                cocktailDTO.Description,
                cocktailDTO.IsAvailable,
                cocktailDTO.Volume,
                cocktailDTO.Calories,
                cocktailDTO.IsAlcoholic,
                cocktailDTO.IsCold
                );
        }

        if (dto is AppetizerDTO appetizerDTO)
        {
            return new Appetizer(
                appetizerDTO.Id,
                appetizerDTO.Name,
                appetizerDTO.Price,
                appetizerDTO.Description,
                appetizerDTO.IsAvailable,
                appetizerDTO.Weight,
                appetizerDTO.IsHot,
                appetizerDTO.SpiceLevel,
                appetizerDTO.IsVegan
                );
        }

        if (dto is BakeryDTO bakeryDTO)
        {
            return new Bakery(
                bakeryDTO.Id,
                bakeryDTO.Name,
                bakeryDTO.Price,
                bakeryDTO.Description,
                bakeryDTO.IsAvailable,
                bakeryDTO.Weight,
                bakeryDTO.Calories,
                bakeryDTO.IsSweet,
                bakeryDTO.IsFresh,
                bakeryDTO.IsVegan
                );
        }

        if (dto is BreakfastDTO breakfastDTO)
        {
            return new Breakfast(
                breakfastDTO.Id,
                breakfastDTO.Name,
                breakfastDTO.Price,
                breakfastDTO.Description,
                breakfastDTO.IsAvailable,
                breakfastDTO.Weight,
                breakfastDTO.Calories,
                breakfastDTO.IncludesDrink,
                breakfastDTO.IsVegan
                );
        }

        if (dto is DessertDTO dessertDTO)
        {
            return new Dessert(
                dessertDTO.Id,
                dessertDTO.Name,
                dessertDTO.Price,
                dessertDTO.Description,
                dessertDTO.IsAvailable,
                dessertDTO.Calories,
                dessertDTO.Weight,
                dessertDTO.IsFrozen,
                dessertDTO.IsVegan
                );
        }

        if (dto is MainCourseDTO mainCourseDTO)
        {
            return new MainCourse(
                mainCourseDTO.Id,
                mainCourseDTO.Name,
                mainCourseDTO.Price,
                mainCourseDTO.Description,
                mainCourseDTO.IsAvailable,
                mainCourseDTO.Weight,
                mainCourseDTO.SpiceLevel,
                mainCourseDTO.IsVegan,
                mainCourseDTO.Calories
                );
        }

        if (dto is SaladDTO saladDTO)
        {
            return new Salad(
                saladDTO.Id,
                saladDTO.Name,
                saladDTO.Price,
                saladDTO.Description,
                saladDTO.IsAvailable,
                saladDTO.Weight,
                saladDTO.IsVegan,
                saladDTO.HasSeafood,
                saladDTO.Calories
                );
        }

        if (dto is SideDishDTO sideDishDTO)
        {
            return new SideDish(
                sideDishDTO.Id,
                sideDishDTO.Name,
                sideDishDTO.Price,
                sideDishDTO.Description,
                sideDishDTO.IsAvailable,
                sideDishDTO.Weight,
                sideDishDTO.Calories,
                sideDishDTO.IsVegan,
                sideDishDTO.IsGlutenFree
                );
        }

        if (dto is SoupDTO soupDTO)
        {
            return new Soup(
                soupDTO.Id,
                soupDTO.Name,
                soupDTO.Price,
                soupDTO.Description,
                soupDTO.IsAvailable,
                soupDTO.Volume,
                soupDTO.IsHot,
                soupDTO.IsVegan,
                soupDTO.SpiceLevel
                );
        }

        throw new Exception("Unknown dishDTO type");
    }
    
    protected Menu DeserializeMenu(MenuDTO dto)
    {
        List<Dish> dishes = new();
        foreach (DishDTO dishDto in dto.Dishes)
        {
            Dish dish = DeserializeDish(dishDto);
            dishes.Add(dish);
        }

        if (!string.IsNullOrWhiteSpace(dto.Season))
        {
            SeasonalMenu seasonalMenu = new SeasonalMenu(dto.Name, dto.Description, dto.Season);
            foreach (Dish dish in dishes) { seasonalMenu.AddDish(dish); }
            return seasonalMenu;
        }

        Menu menu = new Menu(dto.Name, dto.Description);
        foreach (Dish dish in dishes) { menu.AddDish(dish); }
        return menu;
    }
    
    protected Establishment DeserializeEstablishment(EstablishmentDTO dto)
    {
        Menu menu = DeserializeMenu(dto.Menu);
        SeasonalMenu? seasonalMenu = null;
        if (dto.HasSeasonalMenu) 
        { 
            seasonalMenu = (SeasonalMenu)DeserializeMenu(dto.SeasonalMenu);
        }

        if (dto is RestaurantDTO restaurantDTO)
        {
            Restaurant restaurant = new Restaurant(
                restaurantDTO.Id,
                restaurantDTO.Name, 
                restaurantDTO.Address, 
                restaurantDTO.Description, 
                restaurantDTO.IsOpen,
                menu,
                restaurantDTO.Rating,
                restaurantDTO.CuisineType, 
                restaurantDTO.HasMichelinStar,
                restaurantDTO.AverageCheck,
                restaurantDTO.HasDelivery
                );

            if (seasonalMenu != null) { restaurant.AddSeasonalMenu(seasonalMenu); }
            return restaurant;
        }

        if (dto is CafeDTO cafeDTO)
        {

            Cafe cafe = new Cafe(
                cafeDTO.Id,
                cafeDTO.Name,
                cafeDTO.Address,
                cafeDTO.Description,
                cafeDTO.IsOpen,
                menu,
                cafeDTO.Rating,
                cafeDTO.HasBusinessLunch,
                cafeDTO.AverageCheck,
                cafeDTO.HasDelivery,
                cafeDTO.HasOutdoorSeating
                );
            
            if (seasonalMenu != null) { cafe.AddSeasonalMenu(seasonalMenu); }
            return cafe;
        }

        if (dto is CoffeeShopDTO coffeeShopDTO)
        {
            CoffeeShop coffeeShop = new CoffeeShop(
                coffeeShopDTO.Id,
                coffeeShopDTO.Name,
                coffeeShopDTO.Address,
                coffeeShopDTO.Description,
                coffeeShopDTO.IsOpen,
                menu,
                coffeeShopDTO.Rating,
                coffeeShopDTO.HasAlternativeMilk,
                coffeeShopDTO.HasBakery,
                coffeeShopDTO.AverageCoffeePrice,
                coffeeShopDTO.HasWifi
                );
            
            if (seasonalMenu != null) { coffeeShop.AddSeasonalMenu(seasonalMenu); }
            return coffeeShop;
        }

        throw new Exception("Unknown establishmentDTO type");
    }
}