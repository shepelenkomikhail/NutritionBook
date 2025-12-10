using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Api.Filters;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

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
    public async Task<IActionResult> AddItemsToShoppingList([FromBody] ShoppingListDTO newShoppingListDto)
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

    // PUT: api/shoppinglist
    [HttpPut]
    [RequireUserId]
    public async Task<IActionResult> UpdateShoppingList([FromBody] ShoppingListDTO updatedShoppingListDto)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var result = await _shoppingListService.UpdateShoppingListAsync(updatedShoppingListDto, userId);
        if (result == null)
        {
            return BadRequest("Failed to update shopping list.");
        }

        return Ok(result);
    }

    // DELETE: api/shoppinglist/{ingredientId}
    [HttpDelete("{ingredientId}")]
    [RequireUserId]
    public async Task<IActionResult> DeleteItemFromShoppingList(Guid ingredientId)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var result = await _shoppingListService.DeleteItemFromShoppingListAsync(ingredientId, userId);
        if (!result)
        {
            return BadRequest("Failed to delete item from shopping list.");
        }

        return Ok("Item deleted successfully.");
    }

    // DELETE: api/shoppinglist/clear
    [HttpDelete("clear")]
    [RequireUserId]
    public async Task<IActionResult> ClearShoppingList()
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var result = await _shoppingListService.ClearShoppingListAsync(userId);
        if (!result)
        {
            return BadRequest("Failed to clear shopping list.");
        }

        return Ok("Shopping list cleared successfully.");
    }

    // PUT: api/shoppinglist/item/{ingredientId}/bought
    [HttpPut("item/{ingredientId}/bought")]
    [RequireUserId]
    public async Task<IActionResult> UpdateItemIsBoughtStatus(Guid ingredientId, [FromBody] bool isBought)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var result = await _shoppingListService.UpdateItemIsBoughtStatusAsync(userId, ingredientId, isBought);
        if (!result)
        {
            return BadRequest("Failed to update item bought status.");
        }

        return Ok("Item status updated successfully.");
    }

    // PUT: api/shoppinglist/bought
    [HttpPut("bought")]
    [RequireUserId]
    public async Task<IActionResult> UpdateAllItemsIsBoughtStatus([FromBody] bool isBought)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var result = await _shoppingListService.UpdateAllItemsIsBoughtStatusAsync(userId, isBought);
        if (!result)
        {
            return BadRequest("Failed to update bought status for all items.");
        }

        return Ok("All item statuses updated successfully.");
    }
    
    // GET: api/shoppinglist/print
    [HttpGet]
    public async Task<IActionResult> GetShoppingListFile()
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;
        
        var result = await _shoppingListService.GetShoppingListFileAsync(userId);
        if (result == null)
        {
            return NotFound();
        }
        
        return File(result.Value.buffer, result.Value.ContentType);
    }
}