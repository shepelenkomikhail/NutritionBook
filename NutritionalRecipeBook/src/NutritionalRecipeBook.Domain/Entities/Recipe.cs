using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class Recipe: BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int CookingTimeInMin { get; set; }
    public int Servings { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    
    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}