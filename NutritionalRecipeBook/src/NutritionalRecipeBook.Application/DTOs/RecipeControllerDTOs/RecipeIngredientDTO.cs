namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record RecipeIngredientNutrientDTO(
    RecipeDTO RecipeDTO,
    List<IngredientAmountDTO> Ingredients,
    List<NutrientDTO> Nutrients
);