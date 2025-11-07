using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IIngredientService
{
    Task<bool> CreateIngredientAsync(IngredientDTO ingredientDto);
    Task<IngredientDTO> GetIngredientAsync(Guid ingredientId);
    Task<Guid?> GetIngredientIdByNameAsync(string name);
}