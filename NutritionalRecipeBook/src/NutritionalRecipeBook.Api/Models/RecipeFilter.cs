namespace NutritionalRecipeBook.Api.Models;

public record RecipeFilter(
    string? Search,
    int? MinCookingTimeInMin,
    int? MaxCookingTimeInMin,
    int? MinServings,
    int? MaxServings 
);
