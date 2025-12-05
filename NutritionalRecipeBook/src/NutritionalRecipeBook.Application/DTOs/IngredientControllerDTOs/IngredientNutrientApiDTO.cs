namespace NutritionalRecipeBook.Application.DTOs;

public record IngredientNutrientApiDTO(
    string Name,
    int Calories,
    double Proteins,
    double Carbs,
    double Fats,
    string UnitOfMeasure
);