using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Application.Services.Helpers;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services;

public class IngredientService : IIngredientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<IngredientService> _logger;
    private readonly INutrientService _nutrientService;
    
    public IngredientService(
        IUnitOfWork unitOfWork, 
        ILogger<IngredientService> logger,
        INutrientService nutrientService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _nutrientService = nutrientService;
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

            return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "CreateIngredientAsync");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating ingredient.");

            return false;
        }
    }

    public async Task<IngredientDTO?> GetIngredientByIdAsync(Guid ingredientId)
    {
        var existingIngredient = await _unitOfWork.Repository<Ingredient, Guid>().GetByIdAsync(ingredientId);
        if (existingIngredient == null)
        {
            _logger.LogWarning("Ingredient with ID '{Id}' not found.", ingredientId);
            
            return null;
        }
        
        return new IngredientDTO
        (
            existingIngredient.Id, 
            existingIngredient.Name, 
            existingIngredient.IsLiquid
        );
    }
    
    public async Task<Guid?> GetIngredientIdByNameAsync(string name)
    {
        try
        {
            var ingredient = await _unitOfWork.Repository<Ingredient, Guid>()
                .GetSingleOrDefaultAsync(i => i.Name == name);
            if (ingredient == null)
            {
                _logger.LogWarning("Ingredient with name '{IngredienteName}' not found.", name);
                    
                return null;
            }

            return ingredient.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving ingredient name {IngredientName}.", name);
               
            return null;
        }
    }
    
    public async Task<IngredientDTO?> GetIngredientByNameAsync(string name)
    {
        var existingIngredient = await _unitOfWork.Repository<Ingredient, Guid>()
            .GetSingleOrDefaultAsync(i => i.Name == name);
        if (existingIngredient == null)
        {
            _logger.LogWarning("Ingredient with name '{Name}' not found.", name);
            
            return null;
        }
        
        return new IngredientDTO
        (
            existingIngredient.Id,
            existingIngredient.Name,
            existingIngredient.IsLiquid
        );
    }
    
    public async Task<bool> EnsureIngredientExistsAsync(IngredientDTO ingredientDto)
    {
        if (string.IsNullOrWhiteSpace(ingredientDto?.Name))
            return false;

        bool isCreated = await CreateIngredientAsync(ingredientDto);
        if (!isCreated)
        {
            _logger.LogDebug("Ingredient '{Name}' already exists or was not created.", ingredientDto.Name);
        }

        return true;
    }

    public async Task<IEnumerable<IngredientNutrientInfoDTO>> GetAllIngredientsWithNutrientInfoAsync()
    {
        var nutrientInfos = (await _nutrientService.GetAllNutrientsAsync()).ToArray();
        if (nutrientInfos.Length == 0)
        {
            _logger.LogWarning("No nutrient info returned from API");
            
            return Array.Empty<IngredientNutrientInfoDTO>();
        }

        var unitOfMeasures = _unitOfWork.Repository<UnitOfMeasure, Guid>().GetAll();
        
        var uomDictionary = unitOfMeasures
            .ToDictionary(
                uom => uom.Name,
                uom => new UnitOfMeasureDTO(uom.Id, uom.Name, uom.IsLiquidMeasure)
            );

        var result = new List<IngredientNutrientInfoDTO>(nutrientInfos.Length);
        
        foreach (var nutrient in nutrientInfos)
        {
            var nutrientUnit = nutrient.Uom;
            
            if (!uomDictionary.TryGetValue(nutrientUnit, out var uomDto))
            {
                _logger.LogWarning("Unit of measure '{Unit}' missing for nutrient '{Name}'",
                    nutrientUnit, nutrient.Name);
            
                continue;
            }
            
            result.Add(new IngredientNutrientInfoDTO(nutrient, uomDto));
        }

        _logger.LogInformation("Returning {Count} ingredients with nutrient info", result.Count);
        
        return result;
    }

    public async Task<IEnumerable<UnitOfMeasureDTO>> GetMeasures(bool isLiquid)
    {
        var measures = await _unitOfWork.Repository<UnitOfMeasure, Guid>()
            .GetWhereAsync(uom => uom.IsLiquidMeasure == isLiquid);
        
        var measuresDto = measures
            .Select(uom => new UnitOfMeasureDTO(uom.Id, uom.Name, uom.IsLiquidMeasure));
        
        _logger.LogInformation("Returning all measures.");
        
        return measuresDto;
    }
}