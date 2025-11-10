namespace NutritionalRecipeBook.Application.DTOs;

public class RecipeDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int CookingTimeInMin { get; set; }
    public int Servings { get; set; }

    public RecipeDTO()
    {
    }

    internal RecipeDTO(RecipeDTO? recipe)
    {
        ArgumentNullException.ThrowIfNull(recipe, nameof(recipe));
        
        Name = recipe.Name;
        Description = recipe.Description;
        Instructions = recipe.Instructions;
        CookingTimeInMin = recipe.CookingTimeInMin;
        Servings = recipe.Servings;
    }
}