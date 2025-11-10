using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeIngredient recipeUpdateDto);
    Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredient recipeUpdateDto);
    Task<Guid?> GetRecipeIdByNameAsync(string name);
    Task<bool> DeleteRecipeAsync(Guid id);
    Task<RecipeDTO?> GetRecipeByIdAsync(Guid id);
    IEnumerable<RecipeDTO> GetAllRecipesAsync();
    PagedResultDTO<RecipeDTO> GetRecipesAsync(string? search, int pageNumber, int pageSize);
}