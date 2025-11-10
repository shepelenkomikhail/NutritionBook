namespace NutritionalRecipeBook.Application.DTOs;

public class RecipeDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int CookingTimeInMin { get; set; }
    public int Servings { get; set; }
}