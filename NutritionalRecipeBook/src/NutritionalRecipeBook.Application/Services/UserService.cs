using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    
    public UserService(ILogger<UserService> logger, RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<User> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    public async Task<ReturnRegisteredUserDTO?> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        try
        {
            var newUser = new User
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email,
                Name = registerUserDto.Name,
                Surname = registerUserDto.Surname
            };
            
            var result = _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (result.Result.Succeeded)
            {
                var token = await GenerateJwtTokenAsync(newUser);
                
                var registeredUserDto = new ReturnRegisteredUserDTO
                (
                    newUser.Id,
                    newUser.UserName,
                    newUser.Email,
                    newUser.Name,
                    newUser.Surname,
                    token
                );
                
                bool isRoleAdded = await AssignRole(newUser);
                if (!isRoleAdded)
                {
                    _logger.LogWarning("Failed to assign role to user {UserName}.", newUser.UserName);
                }

                _logger.LogInformation("User {UserName } created a new account with password.",
                    newUser.UserName);
                
                return registeredUserDto;
            }

            _logger.LogWarning("User registration failed: "
                               + string.Join(", ", result.Result.Errors.Select(e => e.Description)));
            
            return null;
        }
        catch (Exception e)
        {
             _logger.LogError($"Error registering user: {e.Message}");
             
             return null;
        }
    }
    
    private async Task<string> GenerateJwtTokenAsync(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<bool> AssignRole(User newUser)
    {
        var roleExists = await _roleManager.RoleExistsAsync("User");
        if (!roleExists)
            await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));

        var result = _userManager.AddToRoleAsync(newUser, "User");

        return result.Result.Succeeded;
    }
}