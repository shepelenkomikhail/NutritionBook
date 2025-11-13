using NutritionalRecipeBook.Application.DTOs.AuthControllerDTOs;
using NutritionalRecipeBook.Domain.Entities;

namespace NutritionalRecipeBook.Application.DTOs.Mappers;

public static class UserMapper
{
    public static User EntityToRegisterDto(User user)
    {
        return new User
        {
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname
        };
    }
    
    public static User RegisterDtoToEntity(RegisterUserDTO dto)
    {
        return new User
        {
            UserName = dto.Username.Trim(),
            Email = dto.Email.Trim(),
            Name = dto.Name.Trim(),
            Surname = dto.Surname.Trim()
        };
    }
}