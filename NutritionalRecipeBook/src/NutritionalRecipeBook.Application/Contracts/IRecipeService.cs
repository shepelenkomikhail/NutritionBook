using NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeCreateUpdateDTO recipeUpdateDto);
    Task<bool> UpdateRecipeAsync(Guid id, RecipeCreateUpdateDTO recipeUpdateDto);
    Task<Guid?> GetRecipeIdByNameAsync(string name);
    Task<bool> DeleteRecipeAsync(Guid id);
}