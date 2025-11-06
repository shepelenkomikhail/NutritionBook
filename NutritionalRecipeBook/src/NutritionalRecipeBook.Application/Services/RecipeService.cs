using Microsoft.Extensions.Logging; 
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
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
    
    public async Task<bool> CreateRecipeAsync(Recipe recipe)
    {
        var recipeEntity = new Domain.Entities.Recipe
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            CookingTimeInMin = recipe.CookingTimeInMin,
            Servings = recipe.Servings
        };
        
        await _unitOfWork.Repository<Domain.Entities.Recipe>().InsertAsync(recipeEntity);
        
        _logger.LogInformation("Recipe '{RecipeName}' created successfully.", recipe.Name);
        return await _unitOfWork.SaveAsync();

    }

    public async Task<bool> UpdateRecipeAsync(Guid id, Recipe recipe)
    {
        var existingRecipe = await _unitOfWork.Repository<Domain.Entities.Recipe>().GetByIdAsync(id);
        if (existingRecipe == null) return false;
        
        existingRecipe.Name = recipe.Name;
        existingRecipe.Description = recipe.Description;
        existingRecipe.Instructions = recipe.Instructions;
        existingRecipe.CookingTimeInMin = recipe.CookingTimeInMin;
        existingRecipe.Servings = recipe.Servings;
        
        await _unitOfWork.Repository<Domain.Entities.Recipe>().UpdateAsync(existingRecipe);
        
        _logger.LogInformation("Recipe with ID {RecipeId} updated successfully.", id);
        return await _unitOfWork.SaveAsync();
    }
}