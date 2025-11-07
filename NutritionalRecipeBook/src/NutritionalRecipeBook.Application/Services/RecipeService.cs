using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService(IUnitOfWork unitOfWork, ILogger<RecipeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid?> CreateRecipeAsync(RecipeDTO? recipeDto)
        {
            if (recipeDto == null)
            {
                _logger.LogWarning("CreateRecipeAsync failed: RecipeDTO is null.");
                
                return null;
            }

            if (string.IsNullOrWhiteSpace(recipeDto.Name))
            {
                _logger.LogWarning("CreateRecipeAsync failed: Recipe name is required.");
                
                return null;
            }

            try
            {
                var existingRecipe = await _unitOfWork.Repository<Recipe>().GetSingleOrDefaultAsync(r => r.Name == recipeDto.Name);
                if (existingRecipe != null)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: Recipe '{Name}' already exists.", recipeDto.Name);
                    
                    return null;
                }

                var recipeEntity = new Recipe
                {
                    Name = recipeDto.Name.Trim(),
                    Description = recipeDto.Description.Trim(),
                    Instructions = recipeDto.Instructions.Trim(),
                    CookingTimeInMin = recipeDto.CookingTimeInMin,
                    Servings = recipeDto.Servings
                };

                await _unitOfWork.Repository<Recipe>().InsertAsync(recipeEntity);
                bool isSaved = await _unitOfWork.SaveAsync();

                if (!isSaved)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: SaveAsync returned false.");
                    
                    return null;
                }

                Guid? newRecipeId = await GetRecipeIdByNameAsync(recipeDto.Name);
                _logger.LogInformation("Recipe '{Name}' created successfully with ID {Id}.", recipeEntity.Name, recipeEntity.Id);
                
                return newRecipeId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating recipe '{Name}'.", recipeDto.Name);
                
                return null;
            }
        }

        public async Task<bool> UpdateRecipeAsync(Guid id, RecipeDTO? recipeDto)
        {
            if (recipeDto == null)
            {
                _logger.LogWarning("UpdateRecipeAsync failed: RecipeDTO is null.");
               
                return false;
            }

            try
            {
                var existing = await _unitOfWork.Repository<Recipe>().GetByIdAsync(id);
                if (existing == null)
                {
                    _logger.LogWarning("UpdateRecipeAsync failed: Recipe with ID {Id} not found.", id);
                    
                    return false;
                }

                existing.Name = recipeDto.Name.Trim();
                existing.Description = recipeDto.Description.Trim();
                existing.Instructions = recipeDto.Instructions.Trim();
                existing.CookingTimeInMin = recipeDto.CookingTimeInMin;
                existing.Servings = recipeDto.Servings;

                await _unitOfWork.Repository<Recipe>().UpdateAsync(existing);
                
                var isSaved = await _unitOfWork.SaveAsync();
                if (!isSaved)
                {
                    _logger.LogWarning("UpdateRecipeAsync failed: SaveAsync returned false for recipe ID {Id}.", id);
                    
                    return false;
                }

                _logger.LogInformation("Recipe with ID {Id} updated successfully.", id);
               
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating recipe ID {Id}.", id);
               
                return false;
            }
        }

        public async Task<Guid?> GetRecipeIdByNameAsync(string name)
        {
            try
            {
                var recipe = await _unitOfWork.Repository<Recipe>().GetSingleOrDefaultAsync(r => r.Name == name);
                if (recipe == null)
                {
                    _logger.LogWarning("Recipe with name '{RecipeName}' not found.", name);
                    
                    return null;
                }

                return recipe.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe name {RecipeName}.", name);
               
                return null;
            }
        }
    }
}
