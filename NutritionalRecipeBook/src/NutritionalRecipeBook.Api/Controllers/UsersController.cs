using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Services;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController: ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly RecipeService _recipeService;
    
    public UsersController(ILogger<UsersController> logger, RecipeService recipeService)
    {
        _logger = logger;
        _recipeService = recipeService;
    }
    
    // GET: api/users/{id}/recipes
    [HttpGet]
    public IActionResult Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
        )
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier) ??
            User.FindFirst("sub");

        if (userIdClaim == null || 
            !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogWarning("Missing user id claim or" +
                               "Invalid user id claim value: {Value}", userIdClaim?.Value);
            
            return Unauthorized();
        }

        _logger.LogInformation("Getting recipes for authenticated user {UserId}.", userId);

        var pagedResult = _recipeService.GetRecipesForUserAsync(pageNumber, pageSize, userId);
            
        return Ok(pagedResult);
    }
}