using NutritionalRecipeBook.NutritionWebApi.Contracts;
using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Services;

public sealed class UserService : IUserService
{
    private readonly IJwtService _jwt;
    private readonly IEmailSenderService _emailSender;
    private readonly IConfiguration _configuration;

    public UserService(
        IJwtService jwt,
        IEmailSenderService emailSender,
        IConfiguration configuration)
    {
        _jwt = jwt;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task<(bool Success, int StatusCode, object Response)> RegisterAsync(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Name))
        {
            return (false, 400, "Invalid request");
        }

        if (!request.Email.EndsWith("@nixs.com"))
        {
            return (false, 400, "Only @nixs.com emails allowed");
        }

        var users = _jwt.LoadUsers();

        if (users.Any(u => u.Email == request.Email))
        {
            return (false, 409, "User already exists");
        }

        var user = new User(
            Guid.NewGuid(),
            request.Email,
            request.Name,
            EmailConfirmed: false
        );

        users.Add(user);
        _jwt.SaveUsers(users);

        var token = new EmailToken(
            Token: Guid.NewGuid().ToString("N"),
            UserId: user.Id,
            ExpiresAt: DateTime.UtcNow.AddMinutes(15),
            Used: false
        );

        var tokens = _jwt.LoadTokens();
        tokens.Add(token);
        _jwt.SaveTokens(tokens);

        var apiUrl = _configuration["App:ApiUrl"] ?? string.Empty;
        var confirmationLink = $"{apiUrl}/auth/confirm-email?token={token.Token}";

        try
        {
            await _emailSender.SendEmailAsync(
                user.Email,
                "Confirm your email",
                $"<p>Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.</p>"
            );
        }
        catch (Exception)
        {
            return (false, 500, "Failed to send confirmation email");
        }

        return (true, 200, "Confirmation email sent");
    }

    public Task<(bool Success, int StatusCode, object Response)> ConfirmEmailAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Task.FromResult((false, 400, (object)"Invalid token"));
        }

        var tokens = _jwt.LoadTokens();
        var entry = tokens.SingleOrDefault(t => t.Token == token);

        if (entry is null || entry.Used || entry.ExpiresAt < DateTime.UtcNow)
        {
            return Task.FromResult((false, 400, (object)"Invalid or expired token"));
        }

        var users = _jwt.LoadUsers();
        var user = users.Single(u => u.Id == entry.UserId);

        user = user with { EmailConfirmed = true };
        users[users.FindIndex(u => u.Id == user.Id)] = user;

        entry = entry with { Used = true };
        tokens[tokens.FindIndex(t => t.Token == token)] = entry;

        _jwt.SaveUsers(users);
        _jwt.SaveTokens(tokens);

        var jwt = _jwt.CreateToken(user.Id, user.Email);

        return Task.FromResult((true, 200, (object)new { accessToken = jwt }));
    }
}