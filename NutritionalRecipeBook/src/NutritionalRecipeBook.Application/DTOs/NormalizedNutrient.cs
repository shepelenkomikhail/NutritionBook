namespace NutritionalRecipeBook.Application.DTOs;

public record NormalizedNutrient(
    string Name, 
    string Unit, 
    decimal Amount
);