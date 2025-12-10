using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IShoppingListService
{
    Task<bool> AddItemsToShoppingListAsync(ShoppingListDTO? newShoppingList, Guid userId);
    Task<ShoppingListDTO?> UpdateShoppingListAsync(ShoppingListDTO? updatedShoppingList, Guid userId);
    Task<bool> DeleteItemFromShoppingListAsync(Guid? ingredientId, Guid userId);
    Task<bool> ClearShoppingListAsync(Guid userId);
    
    Task<ShoppingListDTO?> GetShoppingListAsync(Guid userId);
    
    Task<bool> UpdateItemIsBoughtStatusAsync(Guid userId, Guid? ingredientId, bool? isBought);
    Task<bool> UpdateAllItemsIsBoughtStatusAsync(Guid userId, bool? isBought);
}