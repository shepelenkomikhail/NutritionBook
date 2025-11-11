namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record RecipeIngredientDTO(
    RecipeDTO RecipeDTO,
    List<IngredientAmountDTO> Ingredients
);