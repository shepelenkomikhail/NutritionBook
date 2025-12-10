using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IJWTService
{
    Task<string> GenerateJwtTokenAsync(User user,
        IConfiguration _configuration,
        UserManager<User> _userManager
    );
}