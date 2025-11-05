using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain;

public class Recipe: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int CookingTimeInMin { get; set; }
    public int Servings { get; set; }
    
    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
}