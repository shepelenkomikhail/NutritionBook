using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

public class IngredientAmountDTO
{
    public IngredientDTO IngredientDTO { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}