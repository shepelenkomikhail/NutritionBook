namespace NutritionalRecipeBook.Application.DTOs;

public record CommentDTO(
    Guid? Id,
    string Content,
    DateTime CreatedAt,
    Guid RecipeId,
    Guid UserId,
    int Rating
);