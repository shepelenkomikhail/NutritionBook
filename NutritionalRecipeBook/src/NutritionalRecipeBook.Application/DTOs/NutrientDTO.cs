namespace NutritionalRecipeBook.Application.DTOs;

public record NutrientDTO(
    string Name,
    UnitOfMeasureDTO UnitOfMeasureDTO,
    decimal Amount
);