using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;
using NutritionalRecipeBook.Application.DTOs.Mappers;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IEmailSender _emailSender;
    private readonly IJWTService _jwtService;
    
    public UserService(
        ILogger<UserService> logger, 
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<User> userManager,
        IConfiguration configuration,
        IEmailSender emailSender,
        IJWTService jwtService
        )
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _jwtService = jwtService;
    }

    public async Task<ReturnRegisteredUserDTO?> RegisterUserAsync(RegisterUserDTO registerUserDto)
    {
        try
        {
            var newUser = UserMapper.RegisterDtoToEntity(registerUserDto);
            
            var result = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (result.Succeeded)
            {
                var token = await _jwtService.GenerateJwtTokenAsync(
                        newUser,
                        _configuration,
                        _userManager
                    );
                
                bool isRoleAdded = await AssignRole(newUser);
                if (!isRoleAdded)
                {
                    _logger.LogWarning("Failed to assign role to user {UserName}.", newUser.UserName);
                }

                _logger.LogInformation("User {UserName } created a new account with password.",
                    newUser.UserName);
                
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailToken));
                var confirmationLink = $"{_configuration["App:ClientUrl"]}" +
                                       $"/confirm-email?userId={newUser.Id}&token={encodedToken}";
                
                try
                {
                    await _emailSender.SendEmailAsync(
                        newUser.Email!,
                        "Confirm your email",
                        $"<p>Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.</p>"
                    );

                    _logger.LogInformation("Confirmation email sent to {Email}.", newUser.Email);
                }
                catch (Exception emailEx)
                {
                    _logger.LogError(emailEx, "Failed to send confirmation email to {Email}.", newUser.Email);
                }
                
                var registeredUserDto = new ReturnRegisteredUserDTO
                (
                    newUser.Id,
                    newUser.UserName,
                    newUser.Email,
                    newUser.Name,
                    newUser.Surname,
                    token
                );
                
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
    
    public async Task<bool> ConfirmEmailAsync(Guid userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found for email confirmation.", userId);
            
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            _logger.LogInformation("Email for user {UserName} confirmed successfully.", user.UserName);
            
            return true;
        }

        _logger.LogWarning("Email confirmation failed for user {UserName}: "
                           + string.Join(", ", result.Errors.Select(e => e.Description)), user.UserName);
        
        return false;
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