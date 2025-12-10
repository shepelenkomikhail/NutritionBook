namespace NutritionalRecipeBook.Application.DTOs;

public record IngredientUnitOfMeasureDTO(
    IngredientDTO Ingredient,
    string UnitOfMeasure,
    decimal Amount,
    bool IsBought
);