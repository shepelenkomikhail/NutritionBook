using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface ICommentsService
{
    Task<bool> CreateCommentAsync(CommentDTO? newComment);
    
    Task<bool> DeleteCommentAsync(Guid? commentId, Guid userId);
    
    Task<CommentDTO?> GetCommentByIdAsync(Guid? commentId);
    
    Task<IEnumerable<CommentDTO>> GetAllCommentsForRecipeAsync(Guid? recipeId);
}