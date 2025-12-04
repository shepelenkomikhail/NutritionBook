using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutritionalRecipeBook.NutritionWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            var app = builder.Build();
            
            const string fileName = "nutrients.json";

            var nutrientsFilePath = Path.Combine(builder.Environment.ContentRootPath, fileName);
            Nutrient[] nutrients;
            
            if (File.Exists(nutrientsFilePath))
            {
                var json = File.ReadAllText(nutrientsFilePath);
                nutrients = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.NutrientArray)
                            ?? Array.Empty<Nutrient>();
            }
            else
            {
                nutrients = Array.Empty<Nutrient>();
            }

            app.MapGet("/nutrients", () => nutrients);

            app.MapGet("/search", (string? query) =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return Results.BadRequest(new { error = "Query parameter 'query' is required." });
                }

                var q = query.Trim();
                var matches = nutrients
                    .Where(n => n.Name != null && n.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
               
                return Results.Ok(matches);
            });

            app.Run();
        }
    }

    public record Nutrient(
        [property: JsonPropertyName("name")] string? Name, 
        [property: JsonPropertyName("calories")] double Calories, 
        [property: JsonPropertyName("proteins_g")] double ProteinsG, 
        [property: JsonPropertyName("carbs_g")] double CarbsG, 
        [property: JsonPropertyName("fats_g")] double FatsG);

    [JsonSerializable(typeof(Nutrient[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
