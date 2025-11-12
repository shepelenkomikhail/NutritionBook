namespace NutritionalRecipeBook.Api.Models;

public record RegisterModel
(
    string UserName,
    string Password, 
    string Email, 
    string Name,
    string Surname
);