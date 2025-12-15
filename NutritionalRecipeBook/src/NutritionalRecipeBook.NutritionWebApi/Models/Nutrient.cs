using System.Text.Json.Serialization;

namespace NutritionalRecipeBook.NutritionWebApi.Models;

public record Nutrient(
    [property: JsonPropertyName("name")] string? Name, 
    [property: JsonPropertyName("calories")] int Calories, 
    [property: JsonPropertyName("proteins")] double Proteins, 
    [property: JsonPropertyName("carbs")] double Carbs, 
    [property: JsonPropertyName("fats")] double Fats,
    [property: JsonPropertyName("uom")] string UnitOfMeasure);