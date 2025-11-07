using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.ConnectionTables;

public class ShoppingListIngredient: BaseEntity<(Guid RecipeId, Guid IngredientId)>
{
    public Guid ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; } = null!;
    
    public Guid IngredientId { get; set; }
    public virtual Ingredient Ingredient { get; set; } = null!;
    
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}