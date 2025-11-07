using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services;

public class IngredientService : IIngredientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RecipeService> _logger;

    public IngredientService(IUnitOfWork unitOfWork, ILogger<RecipeService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<bool> CreateIngredientAsync(IngredientDTO? ingredientDto)
    {
        if (ingredientDto == null)
        {
            _logger.LogError("IngredientDTO is null.");

            return false;
        }
        
        if (string.IsNullOrWhiteSpace(ingredientDto.Name))
        {
            _logger.LogError("Ingredient name is required.");

            return false;
        }
        
        try
        {
            var existingIngredient = await _unitOfWork.Repository<Ingredient, Guid>()
                .GetSingleOrDefaultAsync(i => i.Name == ingredientDto.Name);
            if (existingIngredient != null)
            {
                _logger.LogWarning("Ingredient '{Name}' already exists.", ingredientDto.Name);

                return false;
            }

            var ingredientEntity = new Ingredient
            {
                Name = ingredientDto.Name.Trim(),
                IsLiquid = ingredientDto.IsLiquid
            };

            await _unitOfWork.Repository<Ingredient, Guid>().InsertAsync(ingredientEntity);
            
            bool isSaved = await _unitOfWork.SaveAsync();
            if (!isSaved)
            {
                _logger.LogError("Failed to save the new ingredient.");

                return false;
            }

            await _unitOfWork.SaveAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating ingredient.");

            return false;
        }
    }

    public async Task<IngredientDTO> GetIngredientAsync(Guid ingredientId)
    {
        var existingIngredient = await _unitOfWork.Repository<Ingredient, Guid>().GetByIdAsync(ingredientId);
        if (existingIngredient == null)
        {
            _logger.LogWarning("Ingredient with ID '{Id}' not found.", ingredientId);
            
            return null;
        }
        
        return new IngredientDTO
        {
            Name = existingIngredient.Name,
            IsLiquid = existingIngredient.IsLiquid
        };
    }
    
    public async Task<Guid?> GetIngredientIdByNameAsync(string name)
    {
        try
        {
            var ingredient = await _unitOfWork.Repository<Ingredient, Guid>().GetSingleOrDefaultAsync(i => i.Name == name);
            if (ingredient == null)
            {
                _logger.LogWarning("Recipe with name '{IngredienteName}' not found.", name);
                    
                return null;
            }

            return ingredient.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving recipe name {IngredientName}.", name);
               
            return null;
        }
    }
}