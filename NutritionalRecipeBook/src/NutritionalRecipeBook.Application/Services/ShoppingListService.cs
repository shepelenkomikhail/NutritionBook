using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.Services.Helpers;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services;

public class ShoppingListService: IShoppingListService
{
    private readonly ILogger<CommentService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public ShoppingListService(ILogger<CommentService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddItemsToShoppingListAsync(ShoppingListDTO? newShoppingList, Guid userId)
    {
        if (newShoppingList == null)
        {
            _logger.LogWarning("ShoppingListDTO is null.");
            
            return false;
        }

        if (newShoppingList?.IngredientUnitOfMeasures == null)
        {
            _logger.LogWarning("Ingredient list is null for user {UserId}", userId);
            
            return false;
        }

        try
        {
            var existingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                .GetSingleOrDefaultAsync(sl => sl.UserId == userId);
            if (existingList == null)
            {
                var insertSucceeded = await _unitOfWork.Repository<ShoppingList, Guid>()
                    .InsertAsync(new ShoppingList { UserId = userId });

                if (!insertSucceeded)
                {
                    _logger.LogWarning("Failed to create new shopping list for user {UserId}", userId);
                    
                    return false;
                }

                await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Create shopping list");

                existingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                    .GetSingleOrDefaultAsync(sl => sl.UserId == userId);

                if (existingList == null)
                {
                    _logger.LogError("Failed to reload newly created shopping list for user {UserId}", userId);
                    
                    return false;
                }
            }

            var shoppingListId = existingList.Id;

            var existingIngredients = (await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().GetWhereAsync(sli => sli.ShoppingListId == shoppingListId))
                .ToDictionary(x => x.IngredientId, x => x);

            foreach (var ingredientDto in newShoppingList.IngredientUnitOfMeasures)
            {
                if (ingredientDto?.Ingredient == null)
                    continue;

                var ingredientEntity = await _unitOfWork
                    .Repository<Ingredient, Guid>()
                    .GetSingleOrDefaultAsync(i => i.Name == ingredientDto.Ingredient.Name);

                var uomEntity = await _unitOfWork
                    .Repository<UnitOfMeasure, Guid>()
                    .GetSingleOrDefaultAsync(uom => uom.Name == ingredientDto.UnitOfMeasure);

                if (ingredientEntity == null || uomEntity == null)
                {
                    _logger.LogWarning("Ingredient {Ingredient} or UoM {Uom} not found",
                        ingredientDto.Ingredient.Name, ingredientDto.UnitOfMeasure);
                    continue;
                }

                if (existingIngredients.TryGetValue(ingredientEntity.Id, out var existingSli))
                {
                    existingSli.Amount += ingredientDto.Amount;
                    
                    await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().UpdateAsync(existingSli);
                }
                else
                {
                    await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().InsertAsync(new ShoppingListIngredient
                    {
                        IngredientId = ingredientEntity.Id,
                        ShoppingListId = shoppingListId,
                        Amount = ingredientDto.Amount,
                        UnitOfMeasureId = uomEntity.Id
                    });
                }
            }

            return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Update shopping list");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while creating/updating shopping list for user {UserId}", userId);
           
            return false;
        }
    }

    public async Task<ShoppingListDTO?> GetShoppingListAsync(Guid userId)
    {
        try
        {
            var shoppingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                .GetSingleOrDefaultAsync(sl => sl.UserId == userId);

            if (shoppingList is null)
            {
                _logger.LogInformation("No shopping list found for user {UserId}", userId);
                return null;
            }

            var shoppingListIngredients = await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
                .GetWhereAsync(sli => sli.ShoppingListId == shoppingList.Id);

            IEnumerable<ShoppingListIngredient> listIngredients = shoppingListIngredients as ShoppingListIngredient[] 
                                                                  ?? shoppingListIngredients.ToArray();
            
            if (!listIngredients.Any())
            {
                return new ShoppingListDTO(shoppingList.Id, userId, Array.Empty<IngredientUnitOfMeasureDTO>());
            }

            var ingredientIds = listIngredients
                .Select(i => i.IngredientId)
                .Distinct()
                .ToList();
            var unitOfMeasureIds = listIngredients
                .Select(i => i.UnitOfMeasureId)
                .Distinct()
                .ToList();

            var ingredients = await _unitOfWork.Repository<Ingredient, Guid>()
                .GetWhereAsync(i => ingredientIds.Contains(i.Id));

            var uoms = await _unitOfWork.Repository<UnitOfMeasure, Guid>()
                .GetWhereAsync(u => unitOfMeasureIds.Contains(u.Id));

            var ingredientLookup = ingredients.ToDictionary(x => x.Id);
            var uomDictionary = uoms.ToDictionary(x => x.Id);

            var ingredientDtos = listIngredients
                .Select(sli => new IngredientUnitOfMeasureDTO(
                new IngredientDTO(
                    ingredientLookup[sli.IngredientId].Id,
                    ingredientLookup[sli.IngredientId].Name,
                    ingredientLookup[sli.IngredientId].IsLiquid),
                uomDictionary[sli.UnitOfMeasureId].Name,
                sli.Amount
            )).ToArray();

            return new ShoppingListDTO(shoppingList.Id, userId, ingredientDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve shopping list for user {UserId}", userId);
            
            return null;
        }
    }
}