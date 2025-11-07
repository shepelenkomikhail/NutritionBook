using NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeCreateDTO recipeDto);
    Task<bool> UpdateRecipeAsync(Guid id, RecipeDTO recipeDto);
    Task<Guid?> GetRecipeIdByNameAsync(string name);
}