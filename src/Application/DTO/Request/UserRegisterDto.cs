using System.Runtime.CompilerServices;
using Domain.Entities;

namespace Application.DTO.Request;

public record UserRegisterDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
)
{
    public static User MapToEntity(UserRegisterDto dto, string password)
    {
        return new User(dto.FirstName,
            dto.LastName,
            dto.Email,
            password);
    }
}