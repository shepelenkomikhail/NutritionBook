namespace NutritionalRecipeBook.Application.DTOs;

public record IngredientDTO(
    Guid? Id, 
    string Name, 
    bool IsLiquid
);