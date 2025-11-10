using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IIngredientService
{
    Task<bool> CreateIngredientAsync(IngredientDTO ingredientDto);
    Task<IngredientDTO?> GetIngredientByIdAsync(Guid ingredientId);
    Task<IngredientDTO?> GetIngredientByNameAsync(string name);
    Task<Guid?> GetIngredientIdByNameAsync(string name);
    Task EnsureIngredientExistsAsync(IngredientDTO ingredientDto);
}