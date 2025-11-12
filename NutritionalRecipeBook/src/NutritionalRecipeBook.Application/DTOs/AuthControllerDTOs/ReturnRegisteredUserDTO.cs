namespace NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;

public record ReturnRegisteredUserDTO
(
    Guid Id,
    string Username,
    string Email,
    string Name,
    string Surname
);