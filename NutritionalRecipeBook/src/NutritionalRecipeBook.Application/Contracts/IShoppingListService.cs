using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IShoppingListService
{
    Task<bool> AddItemsToShoppingList(ShoppingListDTO? newShoppingList, Guid userId);
    Task<ShoppingListDTO?> UpdateShoppingList(ShoppingListDTO? updatedShoppingList, Guid userId);
    Task<bool> DeleteItemFromShoppingList(Guid? ingredientId, Guid userId);
    Task<bool> ClearShoppingList(Guid userId);
    
    Task<ShoppingListDTO?> GetShoppingList(Guid userId);
    
    Task<bool> UpdateItemIsBoughtStatus(Guid userId, Guid? ingredientId, bool? isBought);
    Task<bool> UpdateAllItemsIsBoughtStatus(Guid userId, bool? isBought);
}