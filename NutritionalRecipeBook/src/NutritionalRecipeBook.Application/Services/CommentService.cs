using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.Mappers;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services;

public class CommentService : ICommentsService
{
    private readonly ILogger<CommentService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public CommentService(ILogger<CommentService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateCommentAsync(CommentDTO? commentDto)
    {
        if (commentDto == null)
        {
            _logger.LogWarning("CommentDTO is null.");
            
            return false;
        }

        if (string.IsNullOrWhiteSpace(commentDto.Content))
        {
            _logger.LogWarning("Comment content is required.");

            return false;
        }

        if (commentDto.RecipeId == Guid.Empty)
        {
            _logger.LogWarning("Recipe ID is required to create a comment.");
            return false;
        }

        try
        {
            var recipeExists = await _unitOfWork.Repository<Recipe, Guid>()
                .GetByIdAsync(commentDto.RecipeId) != null;
            
            if (!recipeExists)
            {
                _logger.LogWarning("Cannot create comment: Recipe with ID {RecipeId} not found.", commentDto.RecipeId);
            
                return false;
            }

            var existingComment = await _unitOfWork.Repository<Comment, Guid>()
                .GetWhereAsync(c =>
                c.Content == commentDto.Content &&
                c.RecipeId == commentDto.RecipeId &&
                c.UserId == commentDto.UserId);

            if (existingComment.Any())
            {
                _logger.LogWarning("Comment with content '{Content}' already exists for recipe with ID {RecipeId}.", 
                    commentDto.Content, commentDto.RecipeId);
                
                return false;
            }

            var commentEntity = CommentMapper.ToEntity(commentDto);
            await _unitOfWork.Repository<Comment, Guid>().InsertAsync(commentEntity);

            var existingUserRecipe = await _unitOfWork.Repository<UserRecipe, (Guid, Guid)>()
                .GetSingleOrDefaultAsync(ur => ur.UserId == commentDto.UserId && ur.RecipeId == commentDto.RecipeId);
            
            if (existingUserRecipe is not null)
            {   
                existingUserRecipe.Rating = commentDto.Rating;
                
                await _unitOfWork.Repository<UserRecipe, (Guid, Guid)>().UpdateAsync(existingUserRecipe);
            }
            else
            {
                await _unitOfWork.Repository<UserRecipe, (Guid, Guid)>().InsertAsync(
                    new UserRecipe()
                    {
                        UserId = commentDto.UserId,
                        RecipeId = commentDto.RecipeId,
                        Rating = commentDto.Rating
                    });
            }
            
            var isSaved = await _unitOfWork.SaveAsync();

            if (!isSaved)
            {
                _logger.LogError("Failed to save the new comment.");
                
                return false;
            }
            
            return true;
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while creating comment.");
            
            return false;
        }
    }
    
    public async Task<bool> DeleteCommentAsync(Guid? commentId, Guid userId)
    {
        if (commentId == null)
        {
            _logger.LogWarning("Comment ID is null.");
            
            return false;
        }

        var comment =
            await _unitOfWork.Repository<Comment, Guid>().GetSingleOrDefaultAsync(c => c.Id == commentId.Value);

        if (comment?.UserId != userId)
        {
            _logger.LogWarning("User {UserId} is not authorized to delete comment with ID {CommentId}.", userId, commentId);
            
            return false;
        }
        
        try
        {
            await _unitOfWork.Repository<Comment, Guid>().DeleteAsync(commentId.Value);
            
            var isSaved = await _unitOfWork.SaveAsync();
            
            return isSaved;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while deleting comment.");
            
            return false;
        }
    }

    public async Task<CommentDTO?> GetCommentByIdAsync(Guid? commentId)
    {
        if (commentId == null)
        {
            _logger.LogWarning("Comment ID is null.");
            
            return null;
        }

        var existingComment = await _unitOfWork.Repository<Comment, Guid>().GetByIdAsync(commentId.Value);
        if (existingComment == null)
        {
            _logger.LogWarning("Comment with ID '{Id}' not found.", commentId);
            
            return null;
        }
        
        return CommentMapper.ToDto(existingComment);
    }

    public async Task<IEnumerable<CommentDTO>> GetAllCommentsForRecipeAsync(Guid? recipeId)
    {
        if (recipeId == null)
        {
            _logger.LogWarning("Recipe ID is null.");
            
            return Enumerable.Empty<CommentDTO>();
        }
        
        var comments = await _unitOfWork.Repository<Comment, Guid>()
            .GetWhereAsync(c => c.RecipeId == recipeId);
        
        var ratings = await _unitOfWork.Repository<UserRecipe, (Guid, Guid)>()
            .GetWhereAsync(ur => ur.RecipeId == recipeId);

        var ratingByUser = ratings
            .GroupBy(r => r.UserId)
            .ToDictionary(g => g.Key, g => g.First().Rating);

        return comments.Select(c =>
        {
            ratingByUser.TryGetValue(c.UserId, out var rating);
            return CommentMapper.ToDto(c, rating);
        });
    }
}