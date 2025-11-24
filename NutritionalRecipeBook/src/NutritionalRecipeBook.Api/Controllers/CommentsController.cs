using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Api.Controllers;

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
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDTO newCommentDto)
    {
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
    
    // DELETE api/comments
    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
    {
        var isDeleted = await _commentsService.DeleteCommentAsync(commentId);
        
        if (!isDeleted)
        {
            return BadRequest("Failed to delete comment.");
        }
        
        return Ok("Comment deleted successfully.");
    }
}