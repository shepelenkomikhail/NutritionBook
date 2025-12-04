using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface INutrientService
{
    Task<IEnumerable<IngredientNutrientApiDTO>> GetAllNutrientsAsync();
    Task<IEnumerable<IngredientNutrientApiDTO>> SearchNutrientsAsync(string query);
}