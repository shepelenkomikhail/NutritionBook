using NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeCreateUpdateDTO recipeUpdateDto);
    Task<bool> UpdateRecipeAsync(Guid id, RecipeCreateUpdateDTO recipeUpdateDto);
    Task<Guid?> GetRecipeIdByNameAsync(string name);
    Task<bool> DeleteRecipeAsync(Guid id);
    Task<RecipeDTO?> GetRecipeByIdAsync(Guid id);
    IEnumerable<RecipeDTO> GetAllRecipesAsync();
}