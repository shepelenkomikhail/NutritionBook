namespace NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;

public record ReturnLoggedUserDTO
(
    string UserName,   
    string? Token
);