namespace NutritionalRecipeBook.Application.DTOs;

public record UnitOfMeasureDTO(
    Guid? Id,
    string Name,
    bool IsLiquidMeasure
);