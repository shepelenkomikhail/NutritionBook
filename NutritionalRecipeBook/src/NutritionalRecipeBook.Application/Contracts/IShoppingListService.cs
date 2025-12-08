using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IShoppingListService
{
    Task<bool> CreateShoppingList(ShoppingListDTO? newShoppingList, Guid userId);
    
    Task<ShoppingListDTO?> GetShoppingList(Guid userId);
}