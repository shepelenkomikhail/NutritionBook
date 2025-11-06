using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<bool> CreateRecipeAsync(RecipeDTO recipeDto);
    Task<bool> UpdateRecipeAsync(Guid id, RecipeDTO recipeDto);
    Task<Guid?> GetRecipeIdByNameAsync(string name);
}