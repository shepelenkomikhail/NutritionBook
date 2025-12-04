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
    
    Task<PagedResultDTO<RecipeDTO>> GetRecipesAsync(
            int pageNumber,
            int pageSize,
            RecipeFilterDTO? filterDto = null
    );
    
    Task<PagedResultDTO<RecipeDTO>> GetRecipesForUserAsync(
        int pageNumber,
        int pageSize,
        Guid? userId,
        RecipeFilterDTO? filterDto = null
    );
    
    Task<string?> UploadImageAsync(Stream? fileStream, string originalFileName, string webRootPath);
    
    Task<(Stream Stream, string ContentType)?> GetImageAsync(string fileName, string webRootPath);

    Task<bool> MarkFavoriteRecipeAsync(Guid? recipeId, Guid userId);
    
    Task<bool> UnmarkFavoriteRecipeAsync(Guid recipeId, Guid userId);
    
    Task<PagedResultDTO<RecipeDTO>> GetFavoriteRecipesAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        RecipeFilterDTO? filterDto = null
    );
}
