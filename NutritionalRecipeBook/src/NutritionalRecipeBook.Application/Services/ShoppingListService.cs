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

    public async Task<bool> CreateShoppingList(ShoppingListDTO? newShoppingList, Guid userId)
    {
        if (newShoppingList == null)
        {
            _logger.LogWarning("ShoppingListDTO is null.");
            
            return false;
        }

        try
        {
            var newShoppingListEntity = await _unitOfWork.Repository<ShoppingList, Guid>().InsertAsync(
                new ShoppingList { UserId = newShoppingList.UserId });

            if (newShoppingListEntity == false) 
            {
                _logger.LogWarning("Failed to create shopping list for user {UserId}.", newShoppingList.UserId);
                
                return false;
            }
        
            var newShoppingListId = (await _unitOfWork.Repository<ShoppingList, Guid>().GetSingleOrDefaultAsync(
                sl => sl.UserId == newShoppingList.UserId))!.Id;
            
            foreach (var ingredient in newShoppingList.IngredientUnitOfMeasures)
            {
                var ingredientId = (await _unitOfWork.Repository<Ingredient, Guid>()
                    .GetSingleOrDefaultAsync(i => i.Name == ingredient.Ingredient.Name))?.Id;
                
                var unitOfMeasureId = (await _unitOfWork.Repository<UnitOfMeasure, Guid>()
                    .GetSingleOrDefaultAsync(uom => uom.Name == ingredient.UnitOfMeasure))?.Id;

                if (ingredientId == null || unitOfMeasureId == null)
                {
                    _logger.LogWarning("Ingredient {Ingredient} or Unit of measure {Uom} not found.",
                        ingredient.Ingredient.Name, ingredient.UnitOfMeasure);
                    
                    continue;
                }
                
                await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().InsertAsync(new ShoppingListIngredient
                {
                    IngredientId = ingredientId.Value,
                    ShoppingListId = newShoppingListId,
                    Amount = ingredient.Amount,
                    UnitOfMeasureId = unitOfMeasureId.Value
                });
            }
            
            return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Create shopping list");
        }
        catch (Exception e)
        {
            _logger.LogError("An error occured while create a shopping list - {ErrorMessage}", e.Message);
            
            return false;
        }
    }

    public async Task<ShoppingListDTO?> GetShoppingList(Guid userId)
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