using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class ShoppingList : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual ICollection<ShoppingListIngredient> ShoppingListIngredients { get; set; } = new List<ShoppingListIngredient>();
}