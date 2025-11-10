namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record RecipeIngredient(
    RecipeDTO RecipeDTO,
    List<IngredientAmountDTO> Ingredients
);