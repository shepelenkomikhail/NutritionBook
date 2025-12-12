using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using NutritionalRecipeBook.NutritionWebApi.Contracts;
using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;
    private readonly SymmetricSecurityKey _key;
    private readonly string _usersPath = "Data/users.json";
    private readonly string _tokensPath = "Data/email_tokens.json";

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public JwtService(JwtSettings settings)
    {
        _settings = settings;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SigningKey));
        Directory.CreateDirectory("Data");
        EnsureFile(_usersPath);
        EnsureFile(_tokensPath);
    }

    public string CreateToken(Guid userId, string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public List<User> LoadUsers() =>
        JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_usersPath))!;

    public List<EmailToken> LoadTokens() =>
        JsonSerializer.Deserialize<List<EmailToken>>(File.ReadAllText(_tokensPath))!;

    public void SaveUsers(List<User> users) =>
        File.WriteAllText(_usersPath, JsonSerializer.Serialize(users, JsonOptions));

    public void SaveTokens(List<EmailToken> tokens) =>
        File.WriteAllText(_tokensPath, JsonSerializer.Serialize(tokens, JsonOptions));
    
    private static void EnsureFile(string path)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[]");
        }
    }
}