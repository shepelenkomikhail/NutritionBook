namespace NutritionalRecipeBook.Application.Contracts;

public interface IRecipeService
{
    Task<bool> CreateRecipeAsync(DTOs.Recipe recipe);
    Task<bool> UpdateRecipeAsync(Guid id, DTOs.Recipe recipe);
}