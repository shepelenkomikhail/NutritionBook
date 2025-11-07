using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeService recipeService, ILogger<RecipesController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        // POST: api/recipes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecipeDTO newRecipeDto)
        {
            Guid? newRecipeId = await _recipeService.CreateRecipeAsync(newRecipeDto);
            if (newRecipeId == null)
            {
                return BadRequest("Failed to create recipe.");
            }

            return Created($"/recipes/{newRecipeId}", newRecipeDto);
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RecipeDTO updatedRecipeDto)
        {
            bool updated = await _recipeService.UpdateRecipeAsync(id, updatedRecipeDto);
            if (!updated)
            {
                return BadRequest("Failed to update recipe.");
            }

            return Ok(updatedRecipeDto);
        }
    }
}