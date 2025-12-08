namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record RecipeFilterDTO(
    string? Search,
    int? MinCookingTimeInMin,
    int? MaxCookingTimeInMin,
    int? MinServings,
    int? MaxServings,
    int? MinCaloriesPerServing,
    int? MaxCaloriesPerServing
);