using System.IO;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<Guid?> CreateRecipeAsync(RecipeIngredientDTO recipeUpdateDto);
    
    Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredientDTO recipeUpdateDto);
    
    Task<Guid?> GetRecipeIdByNameAsync(string name);
    
    Task<bool> DeleteRecipeAsync(Guid id);
    
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
}
