using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.ConnectionTables;

public class RecipeIngredient: BaseEntity<(Guid RecipeId, Guid IngredientId)>
{
    public Guid RecipeId { get; set; }
    public virtual Recipe Recipe { get; set; } = null!;
    
    public Guid IngredientId { get; set; }
    public virtual Ingredient Ingredient { get; set; } = null!;
    
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}