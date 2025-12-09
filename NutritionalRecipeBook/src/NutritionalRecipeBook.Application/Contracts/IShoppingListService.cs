using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IShoppingListService
{
    Task<bool> AddItemsToShoppingListAsync(ShoppingListDTO? newShoppingList, Guid userId);
    
    Task<ShoppingListDTO?> GetShoppingListAsync(Guid userId);
}