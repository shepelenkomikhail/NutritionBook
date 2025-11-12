using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NutritionalRecipeBook.Api.Models;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;
using NutritionalRecipeBook.Application.Services;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController: ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    public AuthController(ILogger<AuthController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    // POST: api/auth
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var newUser = new RegisterUserDTO
        (
            model.UserName,
            model.Password,
            model.Email,
            model.Name,
            model.Surname
        );
        
        var registeredUser = await _userService.RegisterUserAsync(newUser);
        if (registeredUser == null)
        {
            return BadRequest("Failed to register user.");
        }

        return Ok(registeredUser);
    }
    
    // GET: api/auth/confirm-email
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
    {
        var result = await _userService.ConfirmEmailAsync(userId, token);

        if (!result)
        {
            return BadRequest("Email confirmation failed.");
        }

        return Ok("Email confirmed successfully!");
    }
}