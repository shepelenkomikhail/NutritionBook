using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using NutritionalRecipeBook.Api.Models;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using System.IO;
using System;

namespace NutritionalRecipeBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipesController> _logger;
        private readonly IWebHostEnvironment _env;

        public RecipesController(IRecipeService recipeService, ILogger<RecipesController> logger, IWebHostEnvironment env)
        {
            _recipeService = recipeService;
            _logger = logger;
            _env = env;
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
        
        // POST: api/recipes/image
        [HttpPost("image")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(10_000_000)] 
        public async Task<IActionResult> UploadImage([FromForm(Name = "file")] IFormFile file)
        {
            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var url = await _recipeService.UploadImageAsync(file.OpenReadStream(), file.FileName, webRootPath);
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest("Failed to upload image.");
            }

            return Created(new Uri(url, UriKind.Relative), new { url });
        }

        // GET: api/recipes/image/{fileName}
        [HttpGet("image/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("File name is required.");
            }

            var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var imagesPath = Path.Combine(webRootPath, "images");
            var fullPath = Path.Combine(imagesPath, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            var extension = Path.GetExtension(fullPath).ToLowerInvariant();
            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream"
            };

            var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(stream, contentType);
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
