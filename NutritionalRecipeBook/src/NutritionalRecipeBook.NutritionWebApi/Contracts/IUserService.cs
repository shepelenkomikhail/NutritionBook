using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Contracts;

public interface IUserService
{
    Task<(bool Success, int StatusCode, object Response)> RegisterAsync(RegisterRequest request);

    Task<(bool Success, int StatusCode, object Response)> ConfirmEmailAsync(string token);
}
