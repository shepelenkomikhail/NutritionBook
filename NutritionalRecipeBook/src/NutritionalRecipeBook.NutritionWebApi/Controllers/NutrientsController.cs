using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.NutritionWebApi.Models;
using NutritionalRecipeBook.NutritionWebApi.Contracts;

namespace NutritionalRecipeBook.NutritionWebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NutrientsController : ControllerBase
    {
        private readonly INutrientsService _nutrientsService;

        public NutrientsController(INutrientsService nutrientsService)
        {
            _nutrientsService = nutrientsService;
        }

        [HttpGet("/search")]
        public async Task<ActionResult<IEnumerable<Nutrient>>> Search([FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Query parameter 'query' is required." });
            }

            var results = await _nutrientsService.SearchAsync(query);
            return Ok(results);
        }
    }
}
