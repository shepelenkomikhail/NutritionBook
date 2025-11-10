using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

public class RecipeCreateUpdateDTO
{
    public RecipeDTO RecipeDTO { get; set; }
    public List<IngredientAmountDTO> Ingredients { get; set; }
}