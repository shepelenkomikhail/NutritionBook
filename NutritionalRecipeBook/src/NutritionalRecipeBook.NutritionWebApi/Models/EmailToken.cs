namespace NutritionalRecipeBook.NutritionWebApi.Models;

public record EmailToken(
    string Token,
    Guid UserId,
    DateTime ExpiresAt,
    bool Used
    );