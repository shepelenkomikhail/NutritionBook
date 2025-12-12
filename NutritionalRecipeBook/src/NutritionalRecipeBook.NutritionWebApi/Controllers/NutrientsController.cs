using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutrientsController : ControllerBase
    {
        private readonly Nutrient[] _nutrients;

        public NutrientsController(Nutrient[] nutrients)
        {
            _nutrients = nutrients ?? Array.Empty<Nutrient>();
        }

        [HttpGet("/nutrients")]
        [Authorize(Policy = "read.nutrients")]
        public ActionResult<IEnumerable<Nutrient>> GetAll()
        {
            return Ok(_nutrients);
        }

        [HttpGet("/search")]
        [Authorize(Policy = "read.nutrients")]
        public ActionResult<IEnumerable<Nutrient>> Search([FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Query parameter 'query' is required." });
            }

            var q = query.Trim();
            var matches = _nutrients
                .Where(n => n.Name != null && n.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            return Ok(matches);
        }
    }
}

