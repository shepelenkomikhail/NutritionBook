using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IShoppingListService
{
    Task<bool> AddItemsToShoppingList(ShoppingListDTO? newShoppingList, Guid userId);
    
    Task<ShoppingListDTO?> GetShoppingList(Guid userId);
    
    Task<bool> MarkItemAsBought(Guid userId, Guid? ingredientId, bool isBought);
    
}