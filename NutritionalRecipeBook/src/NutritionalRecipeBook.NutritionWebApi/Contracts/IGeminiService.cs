namespace NutritionalRecipeBook.NutritionWebApi.Contracts;

public interface IGeminiService
{
    Task<string> GetNutritionDataAsync(string prompt);
}