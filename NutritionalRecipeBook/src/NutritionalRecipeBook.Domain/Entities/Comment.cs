namespace NutritionalRecipeBook.Domain;

public class Comment: BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual Recipe Recipe { get; set; } = null!;
}