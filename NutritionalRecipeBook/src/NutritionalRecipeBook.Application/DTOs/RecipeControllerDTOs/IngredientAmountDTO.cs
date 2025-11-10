using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

public record IngredientAmountDTO(
   IngredientDTO IngredientDTO,
   decimal Amount,
   string Unit
);