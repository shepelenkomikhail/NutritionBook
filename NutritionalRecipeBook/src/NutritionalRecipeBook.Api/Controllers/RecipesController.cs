using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.Contracts.RecipeControllerDTOs;

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
        public async Task<IActionResult> Create([FromBody] RecipeCreateUpdateDTO newRecipeUpdateDto)
        {
            Guid? newRecipeId = await _recipeService.CreateRecipeAsync(newRecipeUpdateDto);
            if (newRecipeId == null)
            {
                return BadRequest("Failed to create recipe.");
            }

            return Created($"/api/recipes/{newRecipeId}", newRecipeUpdateDto);
        }

        // PUT: api/recipes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RecipeCreateUpdateDTO updatedRecipeDto)
        {
            bool isUpdated = await _recipeService.UpdateRecipeAsync(id, updatedRecipeDto);
            if (!isUpdated)
            {
                return BadRequest("Failed to update recipe.");
            }

            return Ok(updatedRecipeDto);
        }
        
        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isDeleted = await _recipeService.DeleteRecipeAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Failed to update recipe.");
            }

            return Ok("Recipe deleted successfully.");
        }
    }
}