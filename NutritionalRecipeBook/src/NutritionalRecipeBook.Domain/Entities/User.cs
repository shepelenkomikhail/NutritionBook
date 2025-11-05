using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    
    public virtual ShoppingList ShoppingList { get; set; } = null!;
    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
    public virtual ICollection<Comment> Comments { get; set; }
}