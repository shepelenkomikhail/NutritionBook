using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IIngredientService
{
    Task<bool> CreateIngredientAsync(IngredientDTO ingredientDto);
    Task<IngredientDTO?> GetIngredientByIdAsync(Guid ingredientId);
    Task<IngredientDTO?> GetIngredientByNameAsync(string name);
    Task<Guid?> GetIngredientIdByNameAsync(string name);
    Task<bool> EnsureIngredientExistsAsync(IngredientDTO ingredientDto);
    Task<IEnumerable<IngredientNutrientInfoDTO>> GetAllIngredientsWithNutrientInfoAsync();
    Task<IEnumerable<UnitOfMeasureDTO>> GetMeasures(bool isLiquid);
}