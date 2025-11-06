using NutritionalRecipeBook.Domain.ConnectionTables;

namespace NutritionalRecipeBook.Domain.Entities;

public class Nutrient: BaseEntity
{
    public string Name {get; set;} = string.Empty;
    public string Unit {get; set;} = string.Empty;
    
    public virtual ICollection<NutrientIngredient> NutrientIngredients { get; set; } = new List<NutrientIngredient>();
}