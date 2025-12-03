using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Api.Filters;
using NutritionalRecipeBook.Api.Models;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private readonly ICommentsService _commentsService;
    
    public CommentsController(ILogger<CommentsController> logger, ICommentsService commentsService)
    {
        _logger = logger;
        _commentsService = commentsService;
    }
    
    // POST: api/comments
    [RequireUserId]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDTO newCommentDto)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        newCommentDto = newCommentDto with { UserId = userId };
        
        var createdComment = await _commentsService.CreateCommentAsync(newCommentDto);

        if (!createdComment)
        {
            return BadRequest("Failed to create comment.");
        }

        return Ok(createdComment);
    }
    
    // GET api/comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsForRecipeAsync(Guid? recipeId)
    {
        var comments = await _commentsService.GetAllCommentsForRecipeAsync(recipeId);
        
        return Ok(comments);
    }

    // GET api/comments/mine
    [RequireUserId]
    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetMyCommentsForRecipeAsync(Guid? recipeId)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;

        var comments = await _commentsService.GetUserCommentsForRecipeAsync(recipeId, userId);

        return Ok(comments);
    }
    
    // DELETE api/comments
    [RequireUserId]
    [HttpDelete]
    public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
    {
        var userId = (Guid)HttpContext.Items[RequireUserIdAttribute.UserIdItemKey]!;
        
        var isDeleted = await _commentsService.DeleteCommentAsync(commentId, userId);
        
        if (!isDeleted)
        {
            return BadRequest("Failed to delete comment.");
        }
        
        return Ok("Comment deleted successfully.");
    }
}