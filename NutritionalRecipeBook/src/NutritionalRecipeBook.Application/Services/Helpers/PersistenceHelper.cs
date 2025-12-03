using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Infrastructure.Contracts;

namespace NutritionalRecipeBook.Application.Services.Helpers;

public static class PersistenceHelper
{
    public static async Task<bool> TrySaveAsync(IUnitOfWork unitOfWork, ILogger logger, string? context = null)
    {
        try
        {
            return await unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                logger.LogError(e, "Error occurred while saving changes to the database.");
            }
            else
            {
                logger.LogError(e, "Error occurred while saving changes to the database. Context: {Context}", context);
            }
           
            return false;
        }
    }
}
