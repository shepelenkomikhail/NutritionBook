namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record IngredientAmountDTO(
   IngredientDTO IngredientDTO,
   decimal Amount,
   string Unit
);