using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Domain.ConnectionTables;

public class UserRecipe: BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    public Guid RecipeId { get; set; }
    public virtual Recipe Recipe { get; set; } = null!;
    
    public bool IsOwner { get; set; }
    public bool IsFavourite { get; set; }
}