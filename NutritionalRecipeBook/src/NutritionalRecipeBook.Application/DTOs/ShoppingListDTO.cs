namespace NutritionalRecipeBook.Application.DTOs;

public record ShoppingListDTO(
    Guid? Id,
    Guid UserId,
    IngredientUnitOfMeasureDTO[] IngredientUnitOfMeasures
);