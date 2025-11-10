using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

public record RecipeCreateUpdateDTO(
    RecipeDTO RecipeDTO,
    List<IngredientAmountDTO> Ingredients
);