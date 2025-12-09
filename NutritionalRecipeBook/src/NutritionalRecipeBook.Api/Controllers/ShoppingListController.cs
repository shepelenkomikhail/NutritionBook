using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Api.Filters;
using NutritionalRecipeBook.Application.Contracts;

namespace NutritionalRecipeBook.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ShoppingListController: ControllerBase
{
    private readonly IShoppingListService _shoppingListService;
    
    public ShoppingListController(IShoppingListService shoppingListService)
    {
        _shoppingListService = shoppingListService;
    }
    
    // POST: api/shoppinglist
    [HttpPost]
    [RequireUserId]
    public async Task<IActionResult> AddItemsToShoppingList([FromBody] Application.DTOs.ShoppingListDTO newShoppingListDto)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;
        
        var result = await _shoppingListService.AddItemsToShoppingListAsync(newShoppingListDto, userId);
        if (!result)
        {
            return BadRequest("Failed to create shopping list.");
        }

        return Created($"/api/shoppinglist/{newShoppingListDto.Id}", newShoppingListDto);
    }
    
    // GET api/shoppinglist
    [HttpGet]
    [RequireUserId]
    public async Task<IActionResult> GetShoppingList()
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;
        
        var shoppingList = await _shoppingListService.GetShoppingListAsync(userId);
        if (shoppingList == null)
        {
            return NotFound("Shopping list not found.");
        }
        
        return Ok(shoppingList);
    }
}