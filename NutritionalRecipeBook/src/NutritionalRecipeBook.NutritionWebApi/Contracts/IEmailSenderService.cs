namespace NutritionalRecipeBook.NutritionWebApi.Contracts;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
