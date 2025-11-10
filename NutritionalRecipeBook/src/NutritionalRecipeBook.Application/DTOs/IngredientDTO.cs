namespace NutritionalRecipeBook.Application.DTOs;

public class IngredientDTO
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsLiquid { get; set; }
    
    public IngredientDTO()
    {
    }

    internal IngredientDTO(IngredientDTO? ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient, nameof(ingredient));
        
        Name = ingredient.Name;
        IsLiquid = ingredient.IsLiquid;
    }
}