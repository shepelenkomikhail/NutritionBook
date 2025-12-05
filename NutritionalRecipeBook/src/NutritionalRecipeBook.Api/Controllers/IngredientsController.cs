using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _ingredientService;
    private readonly INutrientService _nutrientService;
    private readonly ILogger<RecipesController> _logger;

    public IngredientsController(IIngredientService ingredientService, ILogger<RecipesController> logger, INutrientService nutrientService)
    {
        _ingredientService = ingredientService;
        _logger = logger;
        _nutrientService = nutrientService;
    }
    
    // GET api/ingredients
    [HttpGet]
    public async Task<IActionResult> GetAllIngredients()
    {
        //var result = await _ingredientService.GetAllIngredientsWithNutrientInfoAsync();
        var result = await _nutrientService.GetAllNutrientsAsync();
        return Ok(result);
    }
    
    // GET api/ingredients/measures
    [HttpGet("measures/{isLiquid:bool}")]
    public async Task<IActionResult> GetMeasures(bool isLiquid)
    {
        var result = await _ingredientService.GetMeasures(isLiquid);
        
        return Ok(result);
    }
}