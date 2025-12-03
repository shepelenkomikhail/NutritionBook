using System.IO;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeIngredientDTO recipeUpdateDto, Guid userId);
    
    Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredientDTO recipeUpdateDto, Guid userId);
    
    Task<Guid?> GetRecipeIdByNameAsync(string name);
    
    Task<bool> DeleteRecipeAsync(Guid id, Guid userId);
    
    Task<RecipeIngredientDTO?> GetRecipeByIdAsync(Guid id);
    
    IEnumerable<RecipeDTO> GetAllRecipesAsync();
    
    PagedResultDTO<RecipeDTO> GetRecipesAsync(
            int pageNumber,
            int pageSize,
            RecipeFilterDTO? filterDto = null
    );
    
    PagedResultDTO<RecipeDTO> GetRecipesForUserAsync(
        int pageNumber,
        int pageSize,
        Guid? userId
    );
    
    Task<string?> UploadImageAsync(Stream? fileStream, string originalFileName, string webRootPath);

    Task<bool> MarkFavoriteRecipeAsync(Guid? recipeId, Guid? userId);
}
