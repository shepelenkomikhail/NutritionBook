using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.DTOs.Mappers;

public static class RecipeMapper
{
    public static RecipeDTO ToDto(Recipe entity)
    {
        return new RecipeDTO(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Instructions,
            entity.CookingTimeInMin,
            entity.Servings,
            entity.ImageUrl
        );
    }

    public static Recipe ToEntity(RecipeDTO dto)
    {
        return new Recipe
        {
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim() ?? string.Empty,
            Instructions = dto.Instructions?.Trim() ?? string.Empty,
            CookingTimeInMin = dto.CookingTimeInMin,
            Servings = dto.Servings,
            ImageUrl = dto.ImageUrl
        };
    }
}