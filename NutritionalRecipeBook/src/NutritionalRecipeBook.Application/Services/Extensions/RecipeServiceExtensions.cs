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
        }

        return recipeIngredients;
    }

    public static async Task ProcessRecipeIngredientsAsync(
        this RecipeService service,
        Recipe recipeEntity, 
        List<IngredientAmountDTO> ingredientDTOs,
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

        foreach (var ingredientAmount in uniqueIngredients)
        {
            await ingredientService.EnsureIngredientExistsAsync(ingredientAmount.IngredientDTO);

            var ingredientEntity = await unitOfWork.Repository<Ingredient, Guid>()
                .GetSingleOrDefaultAsync(i => 
                    i.Name.ToLower() == ingredientAmount.IngredientDTO.Name.Trim().ToLower());

            if (ingredientEntity == null)
            {
                logger.LogWarning("Ingredient '{Name}' could not be found after creation attempt.", 
                    ingredientAmount.IngredientDTO.Name);
               
                continue;
            }
            
            var unitOfMeasure = await unitOfWork.Repository<UnitOfMeasure, Guid>()
                .GetSingleOrDefaultAsync(u => 
                    u.Name.Equals(ingredientAmount.Unit.Trim(), StringComparison.CurrentCultureIgnoreCase));

            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipeEntity.Id,
                IngredientId = ingredientEntity.Id,
                Amount = ingredientAmount.Amount,
                UnitOfMeasure = unitOfMeasure ?? new UnitOfMeasure
                {
                    Name = ingredientAmount.Unit?.Trim() ?? string.Empty
                }
            };

            await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().InsertAsync(recipeIngredient);
        }
    }

    public static async Task UpdateRecipeIngredientsAsync(
        this RecipeService service,
        Recipe recipeEntity, 
        List<IngredientAmountDTO> ingredientDtos,
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
            await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().DeleteAsync(removeItem);
        }

        foreach (var ingredientAmount in ingredientDtos)
        {
            bool isExists = await ingredientService.EnsureIngredientExistsAsync(ingredientAmount.IngredientDTO);
            if(!isExists)
            {
                logger.LogWarning("UpdateRecipeAsync: Failed to ensure ingredient '{Name}' exists.",
                    ingredientAmount.IngredientDTO.Name);
                
                continue;
            }
            
            var ingredientEntityId = await ingredientService
                .GetIngredientIdByNameAsync(ingredientAmount.IngredientDTO.Name.Trim().ToLower());

            if (ingredientEntityId == null)
            {
                logger.LogWarning(
                    "UpdateRecipeAsync: Ingredient '{Name}' could not be found after creation attempt.",
                    ingredientAmount.IngredientDTO.Name);
                
                continue;
            }

            var existingEntry = await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>()
                .GetSingleOrDefaultAsync(ri => 
                    ri.RecipeId == recipeEntity.Id && ri.IngredientId == ingredientEntityId);

            if (existingEntry == null)
            {
                var unitOfMeasure = await unitOfWork.Repository<UnitOfMeasure, Guid>()
                    .GetSingleOrDefaultAsync(u => 
                        u.Name.Equals(ingredientAmount.Unit.Trim(), StringComparison.CurrentCultureIgnoreCase));
                
                var newLink = new RecipeIngredient
                {
                    RecipeId = recipeEntity.Id,
                    IngredientId = ingredientEntityId.Value,
                    Amount = ingredientAmount.Amount,
                    UnitOfMeasure = unitOfMeasure ?? new UnitOfMeasure
                    {
                        Name = ingredientAmount.Unit?.Trim() ?? string.Empty
                    }
                };

                await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().InsertAsync(newLink);
            }
            else
            {
                existingEntry.Amount = ingredientAmount.Amount;
                existingEntry.UnitOfMeasure.Name = ingredientAmount.Unit?.Trim() ?? string.Empty;
                
                await unitOfWork.Repository<RecipeIngredient, (Guid, Guid)>().UpdateAsync(existingEntry);
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
                r => r.Servings <= filterDto.MaxServings!.Value);
    }
}