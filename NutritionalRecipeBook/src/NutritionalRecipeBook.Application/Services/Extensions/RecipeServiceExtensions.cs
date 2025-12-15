using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Infrastructure.Extensions;

namespace NutritionalRecipeBook.Application.Services.Extensions;

public static class RecipeServiceExtensions
{
    public static async Task<IEnumerable<RecipeIngredient>> LoadRecipeIngredientsAsync(
        this RecipeService service,
        Guid recipeId,
        IUnitOfWork unitOfWork,
        ILogger logger
        )
    {
        var recipeIngredients = (await unitOfWork
                .Repository<RecipeIngredient, (Guid, Guid)>()
                .GetWhereAsync(ri => ri.RecipeId == recipeId))
            .ToList();

        if (recipeIngredients.Count == 0)
        {
            return recipeIngredients;
        }

        var ingredientIds = recipeIngredients
            .Select(ri => ri.IngredientId)
            .Distinct()
            .ToList();

        var ingredients = await unitOfWork
            .Repository<Ingredient, Guid>()
            .GetWhereAsync(i => ingredientIds.Contains(i.Id));

        var ingredientById = ingredients.ToDictionary(i => i.Id);

        var unitOfMeasureIds = recipeIngredients
            .Select(ri => ri.UnitOfMeasureId)
            .Distinct()
            .ToList();

        var unitOfMeasures = await unitOfWork
            .Repository<UnitOfMeasure, Guid>()
            .GetWhereAsync(u => unitOfMeasureIds.Contains(u.Id));

        var uomById = unitOfMeasures.ToDictionary(u => u.Id);

        foreach (var ri in recipeIngredients)
        {
            if (ingredientById.TryGetValue(ri.IngredientId, out var ingredient))
            {
                ri.Ingredient = ingredient;
            }
            else
            {
                logger.LogWarning(
                    "LoadRecipeIngredientsAsync: Ingredient with ID {IngredientId} not found for recipe {RecipeId}.",
                    ri.IngredientId, recipeId);
                ri.Ingredient = null;
            }

            if (uomById.TryGetValue(ri.UnitOfMeasureId, out var uom))
            {
                ri.UnitOfMeasure = uom;
            }
            else
            {
                logger.LogWarning(
                    "LoadRecipeIngredientsAsync: UnitOfMeasure with ID {UomId} not found for recipe {RecipeId}.",
                    ri.UnitOfMeasureId, recipeId);
                ri.UnitOfMeasure = null!;
            }
        }

        return recipeIngredients;
    }

    public static async Task ProcessRecipeIngredientsAsync(
        this RecipeService service,
        Recipe recipeEntity, 
        List<IngredientAmountDTO> ingredientDTOs,
        List<NutrientDTO> Nutrients,
        ILogger logger,
        IUnitOfWork unitOfWork,
        IIngredientService ingredientService
        )
    {
        var uniqueIngredients = ingredientDTOs
            .GroupBy(i => 
                i.IngredientDTO.Name.Trim(), StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .ToList();

        var normalizedNutrients = NormalizeNutrients(Nutrients);

        foreach (var ingredientAmount in uniqueIngredients)
        {
            var (ingredientId, unitOfMeasureId) = await ResolveIngredientAndUnitAsync(
                ingredientAmount,
                ingredientService,
                unitOfWork,
                logger,
                "ProcessRecipeIngredientsAsync");

            if (!ingredientId.HasValue || !unitOfMeasureId.HasValue)
            {
                continue;
            }

            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipeEntity.Id,
                IngredientId = ingredientId.Value,
                Amount = ingredientAmount.Amount,
                UnitOfMeasureId = unitOfMeasureId.Value
            };

            await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().InsertAsync(recipeIngredient);

            if (normalizedNutrients.Count > 0)
            {
                await UpsertNutrientIngredientsAsync(ingredientId.Value, normalizedNutrients, unitOfWork);
            }
        }
    }

    public static async Task UpdateRecipeIngredientsAsync(
        this RecipeService service,
        Recipe recipeEntity, 
        List<IngredientAmountDTO> ingredientDtos,
        List<NutrientDTO> Nutrients,
        ILogger logger,
        IUnitOfWork unitOfWork,
        IIngredientService ingredientService
        )
    {
        var currentEntries = 
            await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>()
            .GetWhereAsync(ri => ri.RecipeId == recipeEntity.Id);

        var newIngredientNames = ingredientDtos
            .Select(i => i.IngredientDTO.Name.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var toRemove = currentEntries
            .Where(ri => ri?.Ingredient != null &&
                         !newIngredientNames
                             .Contains(ri.Ingredient.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();

        foreach (var removeItem in toRemove)
        {
            var niToRemove = await unitOfWork.Repository<NutrientIngredient, (Guid, Guid)>()
                .GetWhereAsync(ni => ni.IngredientId == removeItem.IngredientId);
            foreach (var link in niToRemove)
            {
                await unitOfWork.Repository<NutrientIngredient, (Guid, Guid)>().DeleteAsync(link);
            }

            await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().DeleteAsync(removeItem);
        }

        var normalizedNutrients = NormalizeNutrients(Nutrients);

        foreach (var ingredientAmount in ingredientDtos)
        {
            var (ingredientId, unitOfMeasureId) = await ResolveIngredientAndUnitAsync(
                ingredientAmount,
                ingredientService,
                unitOfWork,
                logger,
                "UpdateRecipeIngredientsAsync");

            if (!ingredientId.HasValue || !unitOfMeasureId.HasValue)
            {
                continue;
            }

            var existingEntry = await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>()
                .GetSingleOrDefaultAsync(ri => 
                    ri.RecipeId == recipeEntity.Id && ri.IngredientId == ingredientId);

            if (existingEntry == null)
            {
                var newLink = new RecipeIngredient
                {
                    RecipeId = recipeEntity.Id,
                    IngredientId = ingredientId.Value,
                    Amount = ingredientAmount.Amount,
                    UnitOfMeasureId = unitOfMeasureId.Value
                };

                await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().InsertAsync(newLink);
            }
            else
            {
                existingEntry.Amount = ingredientAmount.Amount;
                existingEntry.UnitOfMeasureId = unitOfMeasureId.Value;
                
                await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().UpdateAsync(existingEntry);
            }

            if (normalizedNutrients.Count > 0)
            {
                await UpsertNutrientIngredientsAsync(ingredientId.Value, normalizedNutrients, unitOfWork);
            }
        }
    }
    
    public static async Task CheckExistencyAsync(
        this RecipeService service,
        RecipeDTO? recipeDto, 
        ILogger logger, 
        IUnitOfWork unitOfWork
        )
    {
        if (recipeDto == null || string.IsNullOrWhiteSpace(recipeDto.Name))
        {
            logger.LogWarning("CheckExistencyAsync: recipeDto is null or missing a name.");
            throw new ArgumentException("Recipe data is invalid.");
        }

        string normalizedName = recipeDto.Name.Trim().ToLower();

        var existingRecipe = await unitOfWork.Repository<Recipe, Guid>()
            .GetSingleOrDefaultAsync(r => r.Name.ToLower() == normalizedName);

        if (existingRecipe != null)
        {
            logger.LogWarning("CheckExistencyAsync failed: Recipe '{Name}' already exists.", recipeDto.Name);
            
            throw new InvalidOperationException($"Recipe '{recipeDto.Name}' already exists.");
        }
    }
    
    public static async Task CheckExistencyAsync(
        this RecipeService service,
        Guid id, 
        ILogger logger, 
        IUnitOfWork unitOfWork
        )
    {
        var existingRecipe = await unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);

        if (existingRecipe == null)
        {
            logger.LogWarning("CheckExistencyAsync failed: Recipe with ID {Id} not found.", id);
            
            throw new KeyNotFoundException($"Recipe with ID '{id}' does not exist.");
        }
    }

    public static IQueryable<Recipe> ApplyFilter(
        this IQueryable<Recipe> query,
        RecipeFilterDTO? filterDto)
    {
        if (filterDto == null)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(filterDto.Search))
        {
            var search = filterDto.Search.ToLower();
            query = query.Where(r =>
                r.Name.ToLower().Contains(search) ||
                r.Description.ToLower().Contains(search) ||
                r.Instructions.ToLower().Contains(search) ||
                r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains(search))
            );
        }

        return query
            .WhereIfNoTracking(filterDto.MinCookingTimeInMin.HasValue,
                r => r.CookingTimeInMin >= filterDto.MinCookingTimeInMin!.Value)
            .WhereIfNoTracking(filterDto.MaxCookingTimeInMin.HasValue,
                r => r.CookingTimeInMin <= filterDto.MaxCookingTimeInMin!.Value)
            .WhereIfNoTracking(filterDto.MinServings.HasValue,
                r => r.Servings >= filterDto.MinServings!.Value)
            .WhereIfNoTracking(filterDto.MaxServings.HasValue,
                r => r.Servings <= filterDto.MaxServings!.Value)
            .WhereIfNoTracking(filterDto.MinCaloriesPerServing.HasValue,
                r => r.CaloriesPerServing >= filterDto.MinCaloriesPerServing!.Value)
            .WhereIfNoTracking(filterDto.MaxCaloriesPerServing.HasValue,
                r => r.CaloriesPerServing <= filterDto.MaxCaloriesPerServing!.Value);
    }
    
    public static decimal ToGrams(
        this RecipeService service,
        ILogger logger,
        decimal amount, 
        string unitName)
    {
        switch (unitName.ToLowerInvariant())
        {
            case "g":
                return amount;

            case "kg":
                return amount * 1000m;

            case "ml":
                return amount;

            case "l":
                return amount * 1000m;

            case "tsp":
                return amount * 5m;

            case "tbsp":
                return amount * 15m;

            default:
                logger.LogWarning("Unknown unit '{Unit}', assuming grams for calculation.", unitName);
                
                return amount;
        }
    }

    private static List<NormalizedNutrient> NormalizeNutrients(IEnumerable<NutrientDTO>? nutrients)
    {
        if (nutrients == null)
        {
            return new List<NormalizedNutrient>();
        }

        return nutrients
            .Select(n => new NormalizedNutrient(
                n.Name?.Trim() ?? string.Empty,
                n.UnitOfMeasureDTO?.Name?.Trim() ?? string.Empty,
                n.Amount))
            .Where(n => !string.IsNullOrWhiteSpace(n.Name) && !string.IsNullOrWhiteSpace(n.Unit))
            .GroupBy(n => (n.Name.ToLower(), n.Unit.ToLower()))
            .Select(g => g.First())
            .ToList();
    }

    private static async Task<(Guid? IngredientId, Guid? UnitOfMeasureId)> ResolveIngredientAndUnitAsync(
        IngredientAmountDTO ingredientAmount,
        IIngredientService ingredientService,
        IUnitOfWork unitOfWork,
        ILogger logger,
        string context)
    {
        var ensured = await ingredientService.EnsureIngredientExistsAsync(ingredientAmount.IngredientDTO);
        if (!ensured)
        {
            logger.LogWarning("{Context}: Failed to ensure ingredient '{IngredientName}' exists.",
                context, ingredientAmount.IngredientDTO.Name);
            
            return (null, null);
        }

        var normalizedName = ingredientAmount.IngredientDTO.Name.Trim().ToLower();
        var ingredientMatches = await unitOfWork.Repository<Ingredient, Guid>()
            .GetWhereAsync(i => i.Name.ToLower() == normalizedName);
        var ingredientEntity = ingredientMatches.FirstOrDefault();
        if (ingredientMatches.Count() > 1)
        {
            logger.LogWarning("{Context}: Multiple ingredients matched the name '{IngredientName}'. Using the first match.",
                context, ingredientAmount.IngredientDTO.Name);
        }

        if (ingredientEntity == null)
        {
            logger.LogWarning("{Context}: Ingredient '{IngredientName}' not found after ensure attempt.",
                context, ingredientAmount.IngredientDTO.Name);
            
            return (null, null);
        }

        var unitName = (ingredientAmount.Unit ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(unitName))
        {
            logger.LogWarning("{Context}: Unit of measure missing for ingredient '{IngredientName}'.",
                context, ingredientAmount.IngredientDTO.Name);
            
            return (null, null);
        }

        var unitMatches = await unitOfWork.Repository<UnitOfMeasure, Guid>()
            .GetWhereAsync(u => u.Name.ToLower() == unitName.ToLower());
        var unitOfMeasure = unitMatches.FirstOrDefault();
        if (unitMatches.Count() > 1)
        {
            logger.LogWarning("{Context}: Multiple units matched the name '{UnitName}'. Using the first match.",
                context, unitName);
        }

        if (unitOfMeasure == null)
        {
            logger.LogWarning("{Context}: Unit of measure '{UnitName}' not found for ingredient '{IngredientName}'.",
                context, ingredientAmount.Unit?.Trim(), ingredientAmount.IngredientDTO.Name);
            
            return (null, null);
        }

        return (ingredientEntity.Id, unitOfMeasure.Id);
    }

    private static async Task UpsertNutrientIngredientsAsync(
        Guid ingredientId,
        IEnumerable<NormalizedNutrient> nutrients,
        IUnitOfWork unitOfWork)
    {
        foreach (var nutrient in nutrients)
        {
            var normalizedName = nutrient.Name.ToLower();
            var normalizedUnit = nutrient.Unit.ToLower();

            var nutrientEntity = await unitOfWork.Repository<Nutrient, Guid>()
                .GetSingleOrDefaultAsync(x => 
                    x.Name.ToLower() == normalizedName && 
                    x.Unit.ToLower() == normalizedUnit);

            if (nutrientEntity == null)
            {
                nutrientEntity = new Nutrient
                {
                    Name = nutrient.Name,
                    Unit = nutrient.Unit
                };
                
                await unitOfWork.Repository<Nutrient, Guid>().InsertAsync(nutrientEntity);
            }

            var existingNi = await unitOfWork.Repository<NutrientIngredient, (Guid, Guid)>()
                .GetSingleOrDefaultAsync(ni => 
                    ni.NutrientId == nutrientEntity.Id && 
                    ni.IngredientId == ingredientId);

            if (existingNi == null)
            {
                var link = new NutrientIngredient
                {
                    NutrientId = nutrientEntity.Id,
                    IngredientId = ingredientId,
                    IngredientAmountPer100G = nutrient.Amount
                };
                
                await unitOfWork.Repository<NutrientIngredient, (Guid, Guid)>().InsertAsync(link);
            }
            else
            {
                existingNi.IngredientAmountPer100G = nutrient.Amount;
                
                await unitOfWork.Repository<NutrientIngredient, (Guid, Guid)>().UpdateAsync(existingNi);
            }
        }
    }
}