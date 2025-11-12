using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<User> _userManager;
    
    public UserService(ILogger<UserService> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
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
            
            var result = _userManager.CreateAsync(newUser, registerUserDto.Password).Result;
            if (result.Succeeded)
            {
                var registeredUserDto = new ReturnRegisteredUserDTO
                (
                    newUser.Id,
                    newUser.UserName,
                    newUser.Email,
                    newUser.Name,
                    newUser.Surname
                );

                _logger.LogInformation("User {UserName } created a new account with password.",
                    newUser.UserName);
                
                return registeredUserDto;
            }

            _logger.LogWarning("User registration failed: "
                               + string.Join(", ", result.Errors.Select(e => e.Description)));
            
            return null;
        }
        catch (Exception e)
        {
             _logger.LogError($"Error registering user: {e.Message}");
             
             return null;
        }
    }
}