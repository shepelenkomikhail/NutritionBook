using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Services;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        [FromQuery] Guid? userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
        )
    {
        if (userId == null)
        {
            _logger.LogError("User ID is null.");
            
            return BadRequest("Invalid pagination parameters.");
        }
        
        _logger.LogInformation("Getting recipes for user with ID: {UserId}.", userId);
        
        var pagedResult = _recipeService.GetRecipesForUserAsync(pageNumber, pageSize, userId);
            
        return Ok(pagedResult);
    }
}