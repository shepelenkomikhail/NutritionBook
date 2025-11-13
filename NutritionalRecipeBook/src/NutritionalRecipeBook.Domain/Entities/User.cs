using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class User : IdentityUser<Guid>
{
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;
    
    [MinLength(2)]
    public string Surname { get; set; } = string.Empty;
    
    public virtual ShoppingList ShoppingList { get; set; } = null!;
    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}