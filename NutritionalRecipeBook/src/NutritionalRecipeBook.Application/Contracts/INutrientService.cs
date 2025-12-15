using NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface INutrientService
{
    Task<IEnumerable<IngredientNutrientApiDTO>> GetAllNutrientsAsync();
    Task<IEnumerable<IngredientNutrientApiDTO>> SearchNutrientsAsync(string query);
}