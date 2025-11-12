namespace NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;

public record RegisterUserDTO
(
    string Username,
    string Password,
    string Email,
    string Name,
    string Surname
);