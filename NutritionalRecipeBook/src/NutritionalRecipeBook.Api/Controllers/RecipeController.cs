using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Api.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipeController> _logger; 

        public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger) 
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        // POST: RecipeController/Create
        [HttpPost]
        [Route("[controller]/Create")]
        public async Task<ActionResult> Create([FromBody] RecipeDTO? newRecipeDto)
        {
            _logger.LogInformation("Create recipe request received.");
            if (newRecipeDto == null) 
            {
                _logger.LogWarning("Create recipe request failed: recipe is null.");
                return BadRequest("Recipe is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create recipe request failed: invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                bool recipeAsyncResult = await _recipeService.CreateRecipeAsync(newRecipeDto);
                if (!recipeAsyncResult)
                {
                    _logger.LogWarning("Create recipe request failed: service returned false.");
                    return BadRequest("Failed to create recipe.");
                }
            
                _logger.LogInformation("Create recipe request succeeded.");
                Guid? receivedId = await _recipeService.GetRecipeIdByNameAsync(newRecipeDto.Name);

                if (receivedId != null) return Created($"/task/{receivedId}", newRecipeDto);
                
                _logger.LogWarning("Create recipe request failed: could not retrieve recipe ID after creation.");
                return StatusCode(500, "An error occurred while retrieving the created recipe ID.");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create recipe request failed with exception.");
                return StatusCode(500, "An error occurred while creating the recipe.");
            }
        }
        
        // PUT: RecipeController/Update/5
        [HttpPut]
        [Route("[controller]/Update/{id}")]
        public async Task<ActionResult> Update([FromBody] RecipeDTO? updatedRecipeDto, Guid id)
        {
            _logger.LogInformation("Update recipe request received.");
            if (updatedRecipeDto == null) 
            {
                _logger.LogWarning("Update recipe request failed: recipe is null.");
                return BadRequest("Recipe is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update recipe request failed: invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var recipeAsyncResult = await _recipeService.UpdateRecipeAsync(id, updatedRecipeDto);
                if (!recipeAsyncResult)
                {
                    _logger.LogWarning("Update recipe request failed: service returned false.");
                    return BadRequest("Failed to update recipe.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update recipe request failed while checking for existing recipe name.");
                return StatusCode(500, "An error occurred while validating the recipe name.");
            }
            
            _logger.LogInformation("Update recipe request succeeded.");
            return Ok(updatedRecipeDto);
        }
    }
}