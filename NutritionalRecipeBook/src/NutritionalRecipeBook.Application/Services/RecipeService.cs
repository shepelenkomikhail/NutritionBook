using Microsoft.Extensions.Logging; 
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services;

public class RecipeService: IRecipeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RecipeService> _logger;
    
    public RecipeService(IUnitOfWork unitOfWork, ILogger<RecipeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<bool> CreateRecipeAsync(RecipeDTO recipeDto)
    {
        var recipeEntity = new Recipe
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            Instructions = recipeDto.Instructions,
            CookingTimeInMin = recipeDto.CookingTimeInMin,
            Servings = recipeDto.Servings
        };
        
        await _unitOfWork.Repository<Recipe>().InsertAsync(recipeEntity);
        
        _logger.LogInformation("Recipe '{RecipeName}' created successfully.", recipeDto.Name);
        return await _unitOfWork.SaveAsync();

    }

    public async Task<bool> UpdateRecipeAsync(Guid id, RecipeDTO recipeDto)
    {
        var existingRecipe = await _unitOfWork.Repository<Recipe>().GetByIdAsync(id);
        if (existingRecipe == null) return false;
        
        existingRecipe.Name = recipeDto.Name;
        existingRecipe.Description = recipeDto.Description;
        existingRecipe.Instructions = recipeDto.Instructions;
        existingRecipe.CookingTimeInMin = recipeDto.CookingTimeInMin;
        existingRecipe.Servings = recipeDto.Servings;
        
        await _unitOfWork.Repository<Domain.Entities.Recipe>().UpdateAsync(existingRecipe);
        
        _logger.LogInformation("Recipe with ID {RecipeId} updated successfully.", id);
        return await _unitOfWork.SaveAsync();
    }
    
    public async Task<Guid?> GetRecipeIdByNameAsync(string name)
    {
        var recipe = await _unitOfWork.Repository<Recipe>().GetSingleOrDefaultAsync(r => r.Name == name);
        
        if (recipe == null)
        {
            _logger.LogWarning("Recipe with name '{RecipeName}' not found.", name);
            return null;
        }
        
        _logger.LogInformation("Recipe with name '{RecipeName}' found with ID {RecipeId}.", name, recipe.Id);
        return recipe.Id;
    }
}