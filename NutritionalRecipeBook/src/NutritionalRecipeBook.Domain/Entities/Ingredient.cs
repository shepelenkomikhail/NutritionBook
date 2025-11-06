using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class Ingredient: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsLiquid { get; set; }
    
    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public virtual ICollection<NutrientIngredient> NutrientIngredients { get; set; } = new List<NutrientIngredient>();
    public virtual ICollection<ShoppingListIngredient> ShoppingListIngredients { get; set; } = new List<ShoppingListIngredient>();
}