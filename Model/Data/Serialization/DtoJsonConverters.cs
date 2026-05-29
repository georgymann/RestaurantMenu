using Model.Data.Serialization.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model.Data.Serialization;

public class DishDtoJsonConverter : JsonConverter<DishDTO>
{
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, DishDTO? value, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }

    public override DishDTO ReadJson(JsonReader reader, Type objectType, DishDTO? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string kind = obj["Kind"]?.ToString() ?? string.Empty;

        DishDTO dto = kind switch
        {
            "Beer" => new BeerDTO(),
            "Drink" => new DrinkDTO(),
            "Tea" => new TeaDTO(),
            "Coffee" => new CoffeeDTO(),
            "Cocktail" => new CocktailDTO(),
            "Appetizer" => new AppetizerDTO(),
            "Bakery" => new BakeryDTO(),
            "Breakfast" => new BreakfastDTO(),
            "Dessert" => new DessertDTO(),
            "MainCourse" => new MainCourseDTO(),
            "Salad" => new SaladDTO(),
            "SideDish" => new SideDishDTO(),
            "Soup" => new SoupDTO(),
            _ => throw new NotSupportedException($"Неизвестный тип блюда в JSON: '{kind}'.")
        };

        using JsonReader objReader = obj.CreateReader();
        serializer.Populate(objReader, dto);
        return dto;
    }
}

public class EstablishmentDtoJsonConverter : JsonConverter<EstablishmentDTO>
{
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, EstablishmentDTO? value, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }

    public override EstablishmentDTO ReadJson(JsonReader reader, Type objectType, EstablishmentDTO? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string kind = obj["Kind"]?.ToString() ?? string.Empty;

        EstablishmentDTO dto = kind switch
        {
            "Restaurant" => new RestaurantDTO(),
            "Cafe" => new CafeDTO(),
            "CoffeeShop" => new CoffeeShopDTO(),
            _ => throw new NotSupportedException($"Неизвестный тип заведения в JSON: '{kind}'.")
        };

        using JsonReader objReader = obj.CreateReader();
        serializer.Populate(objReader, dto);
        return dto;
    }
}
