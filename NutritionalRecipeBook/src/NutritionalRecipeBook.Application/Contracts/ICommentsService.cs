using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface ICommentsService
{
    Task<bool> CreateCommentAsync(CommentDTO? commentDto);
    
    Task<bool> DeleteCommentAsync(Guid? commentId);
    
    Task<CommentDTO?> GetCommentByIdAsync(Guid? commentId);
    
    Task<IEnumerable<CommentDTO>> GetAllCommentsForRecipeAsync(Guid? recipeId);
}