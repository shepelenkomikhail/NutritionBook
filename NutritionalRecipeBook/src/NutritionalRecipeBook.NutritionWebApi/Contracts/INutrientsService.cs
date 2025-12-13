using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Contracts;

public interface INutrientsService
{
    Task<IEnumerable<Nutrient>> SearchAsync(string query);
}