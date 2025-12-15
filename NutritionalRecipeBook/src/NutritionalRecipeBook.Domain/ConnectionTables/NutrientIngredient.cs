using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.ConnectionTables;

public class NutrientIngredient: BaseEntity<(Guid RecipeId, Guid IngredientId)>
{
    public Guid NutrientId { get; set; }
    public virtual Nutrient Nutrient { get; set; } = null!;
    
    public Guid IngredientId { get; set; }
    public virtual Ingredient Ingredient { get; set; } = null!;
    
    public decimal IngredientAmountPer100G { get; set; }
}