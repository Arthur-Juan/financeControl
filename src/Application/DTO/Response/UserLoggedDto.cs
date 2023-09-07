using Domain.Entities;

namespace Application.DTO.Response;

public record UserLoggedDto(
    string? FirstName,
    string? LastName,
    string? Email,
    string Token
    )
{
    public static UserLoggedDto MapFromEntity(User? entity,string token)
    {
        return new UserLoggedDto(
            entity?.FirstName,
            entity?.LastName,
            entity?.Email,
            token
            );
    }
}