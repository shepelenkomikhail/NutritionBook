using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.Mappers;
using NutritionalRecipeBook.Application.Services.Helpers;
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

        try
        {
            var recipeExists = await _unitOfWork.Repository<Recipe, Guid>()
                .GetByIdAsync(commentDto.RecipeId) != null;

            if (!recipeExists)
            {
                _logger.LogWarning("Cannot create comment: Recipe with ID {RecipeId} not found.", commentDto.RecipeId);
                
                return false;
            }

            var duplicate = await _unitOfWork.Repository<Comment, Guid>()
                .GetSingleOrDefaultAsync(c => c.UserId == commentDto.UserId
                                           && c.RecipeId == commentDto.RecipeId
                                           && c.Content == commentDto.Content);

            if (duplicate != null)
            {
                _logger.LogWarning("Duplicate comment content for user {UserId} on recipe {RecipeId}.",
                    commentDto.UserId, commentDto.RecipeId);

                return false;
            }

            var commentEntity = CommentMapper.ToEntity(commentDto);
            commentEntity.CreatedAt = DateTime.UtcNow;
            commentEntity.Rating = commentDto.Rating;

            await _unitOfWork.Repository<Comment, Guid>().InsertAsync(commentEntity);

            var existingUserRecipe = await _unitOfWork.Repository<UserRecipe, Guid>()
                .GetSingleOrDefaultAsync(ur => ur.UserId == commentDto.UserId 
                                               && ur.RecipeId == commentDto.RecipeId);

            if (existingUserRecipe == null)
            {
                await _unitOfWork.Repository<UserRecipe, Guid>().InsertAsync(new UserRecipe
                {
                    UserId = commentDto.UserId,
                    RecipeId = commentDto.RecipeId,
                });
            }
            
            var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "CreateCommentAsync");

            return result;
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

        var comment = await _unitOfWork.Repository<Comment, Guid>()
            .GetSingleOrDefaultAsync(c => c.Id == commentId.Value);

        if (comment?.UserId != userId)
        {
            _logger.LogWarning("User {UserId} is not authorized to delete comment with ID {CommentId}.",
                userId, commentId);
            
            return false;
        }
        
        try
        {
            await _unitOfWork.Repository<Comment, Guid>().DeleteAsync(commentId.Value);

            var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "CreateCommentAsync");
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while deleting comment.");
            
            return false;
        }
    }

    public async Task<IEnumerable<CommentDTO>> GetAllCommentsForRecipeAsync(Guid? recipeId)
    {
        if (recipeId == null)
        {
            _logger.LogWarning("Recipe ID is null.");
            
            return Enumerable.Empty<CommentDTO>();
        }

        var comments = (await _unitOfWork.Repository<Comment, Guid>()
                .GetWhereAsync(c => c.RecipeId == recipeId))
            .OrderByDescending(c => c.CreatedAt)
            .ToList();

        return comments.Select(CommentMapper.ToDto);
    }

    public async Task<IEnumerable<CommentDTO>> GetUserCommentsForRecipeAsync(Guid? recipeId, Guid userId)
    {
        if (recipeId == null)
        {
            _logger.LogWarning("Recipe ID is null.");
           
            return Enumerable.Empty<CommentDTO>();
        }

        var comments = (await _unitOfWork.Repository<Comment, Guid>()
                .GetWhereAsync(c => c.RecipeId == recipeId && c.UserId == userId))
            .OrderBy(c => c.CreatedAt)
            .ToList();

        if (comments.Count == 0)
        {
            return Enumerable.Empty<CommentDTO>();
        }

        return comments.Select(CommentMapper.ToDto);
    }
}