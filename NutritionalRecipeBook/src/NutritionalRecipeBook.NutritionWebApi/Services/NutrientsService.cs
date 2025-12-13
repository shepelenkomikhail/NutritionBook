using System.Text.Json;
using NutritionalRecipeBook.NutritionWebApi.Context;
using NutritionalRecipeBook.NutritionWebApi.Contracts;
using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Services;

public class NutrientsService : INutrientsService
{
    private readonly IGeminiService _geminiService;

    public NutrientsService(IGeminiService geminiService)
    {
        _geminiService = geminiService ?? throw new ArgumentNullException(nameof(geminiService));
    }

    public async Task<IEnumerable<Nutrient>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Array.Empty<Nutrient>();
        }  
        
        var exampleJson =
            """[{"name":"Apple","calories":52,"proteins":0.3,"carbs":14,"fats":0.2,"uom":"g"}]""";

        var prompt = $"""
                      Identify the single most likely food item matching the query: "{query}".
                      Return a JSON array containing exactly one object with nutritional data for 100g.
                      Example format:
                      {exampleJson}
                      """;

        try
        {
            var responseJson = await _geminiService.GetNutritionDataAsync(prompt);

            if (string.IsNullOrWhiteSpace(responseJson))
            {
                return Array.Empty<Nutrient>();
            }

            responseJson = CleanJson(responseJson);
            
            var parsed = JsonSerializer.Deserialize(responseJson, AppJsonSerializerContext.Default.NutrientArray);

            if (parsed != null && parsed.Length > 0)
            {
                return parsed;
            }

            return Array.Empty<Nutrient>();
        }
        catch (Exception)
        {
            return Array.Empty<Nutrient>();
        }
    }

    private static string CleanJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return json;
        
        json = json.Replace("```json", "", StringComparison.OrdinalIgnoreCase);
        json = json.Replace("```", "", StringComparison.OrdinalIgnoreCase);
        
        return json.Trim();
    }
}