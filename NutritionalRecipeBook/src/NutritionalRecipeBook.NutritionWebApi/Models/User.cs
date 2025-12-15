namespace NutritionalRecipeBook.NutritionWebApi.Models;

public record User(
    Guid Id,
    string Email,
    string Name,
    bool EmailConfirmed
);