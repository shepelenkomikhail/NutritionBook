namespace NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;

public record PagedResultDTO<T>(
    IEnumerable<T> Items, 
    int? TotalCount,
    int? PageNumber, 
    int? PageSize 
);