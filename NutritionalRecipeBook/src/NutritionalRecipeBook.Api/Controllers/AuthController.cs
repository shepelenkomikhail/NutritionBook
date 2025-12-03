using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Api.Models;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;

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
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO newUserDTO)
    {
        var registeredUser = await _userService.RegisterUserAsync(newUserDTO);
        if (registeredUser == null)
        {
            return BadRequest("Failed to register user.");
        }

        return Ok(registeredUser);
    }
    
    // POST: api/auth/login
    [HttpPost("login")]    
    public async Task<IActionResult> Login([FromBody] LoginModel model)     
    {
        var loginUser = new LoginUserDTO(model.UserName, model.Password);    
        
        var userCredentials = await _userService.LoginUserAsync(loginUser);         
        
        if (userCredentials.Token == null)         
        {
            return Unauthorized("Invalid username or password.");
        }          
        
        return Ok(userCredentials);
    }

    // GET: /api/auth/users/{userId}/email-confirmation?token=...
    [HttpGet("users/{userId}/email-confirmation")]
    public async Task<IActionResult> ConfirmEmailGet(Guid userId, [FromQuery] string token)
    {
        var result = await _userService.ConfirmEmailAsync(userId, token);

        if (!result)
        {
            return BadRequest("Email confirmation failed.");
        }

        return Ok("Email confirmed successfully!");
    }
}