using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RecipeService> _logger;
        private readonly IIngredientService _ingredientService;

        public RecipeService(IUnitOfWork unitOfWork, ILogger<RecipeService> logger, IIngredientService ingredientService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _ingredientService = ingredientService;
        }

        public async Task<Guid?> CreateRecipeAsync(RecipeCreateDTO? recipeDto)
        {
            if (recipeDto == null)
            {
                _logger.LogWarning("CreateRecipeAsync failed: RecipeDTO is null.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(recipeDto.RecipeDTO.Name))
            {
                _logger.LogWarning("CreateRecipeAsync failed: Recipe name is required.");
                return null;
            }

            try
            {
                var existingRecipe = await _unitOfWork.Repository<Recipe, Guid>()
                    .GetSingleOrDefaultAsync(r => r.Name == recipeDto.RecipeDTO.Name.Trim());

                if (existingRecipe != null)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: Recipe '{Name}' already exists.", recipeDto.RecipeDTO.Name);
                    
                    return null;
                }

                var recipeEntity = new Recipe
                {
                    Name = recipeDto.RecipeDTO.Name.Trim(),
                    Description = recipeDto.RecipeDTO.Description.Trim(),
                    Instructions = recipeDto.RecipeDTO.Instructions.Trim(),
                    CookingTimeInMin = recipeDto.RecipeDTO.CookingTimeInMin,
                    Servings = recipeDto.RecipeDTO.Servings
                };

                await _unitOfWork.Repository<Recipe, Guid>().InsertAsync(recipeEntity);
                await _unitOfWork.SaveAsync(); 

                if (recipeDto?.Ingredients != null && recipeDto.Ingredients.Any())
                {
                    foreach (var ingredientAmount in recipeDto.Ingredients)
                    {
                        bool isIngredientCreated = await _ingredientService.CreateIngredientAsync(ingredientAmount.IngredientDTO);
                        if (!isIngredientCreated)
                        {
                            _logger.LogInformation("Ingredient '{Name}' already exists or was not newly created.", ingredientAmount.IngredientDTO.Name);
                        }
                        
                        var ingredientEntity = await _unitOfWork.Repository<Ingredient, Guid>()
                            .GetSingleOrDefaultAsync(i => i.Name == ingredientAmount.IngredientDTO.Name);

                        if (ingredientEntity == null)
                        {
                            _logger.LogWarning("CreateRecipeAsync failed: Ingredient '{Name}' could not be found after creation attempt.", ingredientAmount.IngredientDTO.Name);
                            
                            continue;
                        }

                        var recipeIngredient = new RecipeIngredient
                        {
                            RecipeId = recipeEntity.Id,
                            IngredientId = ingredientEntity.Id,
                            Recipe = recipeEntity,
                            Ingredient = ingredientEntity,
                            Amount = ingredientAmount.Amount,
                            Unit = ingredientAmount.Unit.Trim()
                        };

                        await _unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().InsertAsync(recipeIngredient);
                    }
                }

                bool isSaved = await _unitOfWork.SaveAsync();
                if (!isSaved)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: SaveAsync returned false.");
                    return null;
                }

                _logger.LogInformation("Recipe '{Name}' created successfully with ID {Id}.", recipeEntity.Name, recipeEntity.Id);
                return recipeEntity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating recipe '{Name}'.", recipeDto.RecipeDTO.Name);
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
                var existing = await _unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);
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

                await _unitOfWork.Repository<Recipe, Guid>().UpdateAsync(existing);
                
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
                var recipe = await _unitOfWork.Repository<Recipe, Guid>().GetSingleOrDefaultAsync(r => r.Name == name);
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
