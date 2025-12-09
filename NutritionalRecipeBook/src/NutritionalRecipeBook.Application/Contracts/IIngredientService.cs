using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IIngredientService
{
    Task<bool> EnsureIngredientExistsAsync(IngredientDTO ingredientDto);
    
    Task<IEnumerable<IngredientNutrientInfoDTO>> GetAllIngredientsWithNutrientInfoAsync();
    Task<IEnumerable<UnitOfMeasureDTO>> GetMeasures(bool isLiquid);
}