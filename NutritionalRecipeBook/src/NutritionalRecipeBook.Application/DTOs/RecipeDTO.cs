namespace NutritionalRecipeBook.Application.DTOs;

public record RecipeDTO(
    Guid? Id,
    string Name,
    string Description,
    string Instructions,
    int CookingTimeInMin,
    int Servings, 
    string ImageUrl
);