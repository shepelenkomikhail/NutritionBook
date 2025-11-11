using Microsoft.AspNetCore.Identity;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public virtual ShoppingList ShoppingList { get; set; } = null!;
    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}