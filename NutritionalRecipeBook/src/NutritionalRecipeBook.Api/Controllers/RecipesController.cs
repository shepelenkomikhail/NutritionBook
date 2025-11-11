using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Api.Models;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

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
        public async Task<IActionResult> Create([FromBody] RecipeIngredientDTO newRecipeUpdateDto)
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
        public async Task<IActionResult> Update(Guid id, [FromBody] RecipeIngredientDTO updatedRecipeDto)
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
        
        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return BadRequest("Failed to get recipe.");
            }

            return Ok(recipe);
        }
        
        // GET: api/recipes
        [HttpGet]
        public IActionResult Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] RecipeFilter? filter = null)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Invalid pagination parameters.");
            }

            var filterDto = new RecipeFilterDTO(
                filter?.Search,
                filter?.MinCookingTimeInMin, 
                filter?.MaxCookingTimeInMin,
                filter?.MinServings,
                filter?.MaxServings
            );
            var pagedResult = _recipeService.GetRecipesAsync(pageNumber, pageSize, filterDto);
            
            return Ok(pagedResult);
        }
    }
}
