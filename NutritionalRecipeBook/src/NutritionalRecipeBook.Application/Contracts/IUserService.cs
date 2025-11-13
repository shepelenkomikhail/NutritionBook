using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;

namespace NutritionalRecipeBook.Application.Contracts;

public interface IUserService
{
    Task<ReturnRegisteredUserDTO?> RegisterUserAsync(RegisterUserDTO registerUserDto);
    Task<bool> ConfirmEmailAsync(Guid userId, string token);
    Task<ReturnLoggedUserDTO> LoginUserAsync(LoginUserDTO loginUserDto);
}