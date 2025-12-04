using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class UnitOfMeasure : BaseEntity<Guid>
{
    public string Name {get; set;} = string.Empty;
    
    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}