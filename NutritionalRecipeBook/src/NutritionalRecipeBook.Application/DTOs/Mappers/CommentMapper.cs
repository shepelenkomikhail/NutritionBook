using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.DTOs.Mappers;

public static class CommentMapper
{
    public static CommentDTO ToDto(Comment entity)
    {
        return new CommentDTO(
            entity.Id,
            entity.Content,
            entity.CreatedAt,
            entity.RecipeId,
            entity.UserId
        );
    }

    public static Comment ToEntity(CommentDTO dto)
    {
        return new Comment
        {
            Content = dto.Content.Trim(),
            CreatedAt = dto.CreatedAt,
            RecipeId = dto.RecipeId,
            UserId = dto.UserId
        };
    }
}