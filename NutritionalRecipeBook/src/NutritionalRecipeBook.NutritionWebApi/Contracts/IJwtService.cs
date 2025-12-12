using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Contracts;

public interface IJwtService
{
    string CreateToken(Guid userId, string email);

    List<User> LoadUsers();

    List<EmailToken> LoadTokens();

    void SaveUsers(List<User> users);

    void SaveTokens(List<EmailToken> tokens);
}
