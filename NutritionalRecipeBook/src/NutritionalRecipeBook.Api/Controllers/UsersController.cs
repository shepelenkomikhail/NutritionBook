using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController: ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IRecipeService _recipeService;
    
    public UsersController(ILogger<UsersController> logger, IRecipeService recipeService)
    {
        _logger = logger;
        _recipeService = recipeService;
    }
    
    // GET: api/users/recipes
    [HttpGet("recipes")]
    public IActionResult Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
        )
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier) ??
            User.FindFirst("sub");

        _logger.LogInformation(
            "User authenticated: {IsAuth}. Claims count: {ClaimsCount}. NameIdentifier: {NameId}",
            User?.Identity?.IsAuthenticated ?? false,
            User?.Claims?.Count() ?? 0,
            userIdClaim?.Value
        );

        if (userIdClaim == null || 
            !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var pagedResult = _recipeService.GetRecipesForUserAsync(pageNumber, pageSize, userId);
            
        return Ok(pagedResult);
    }
}