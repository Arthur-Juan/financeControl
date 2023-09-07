namespace Application.DTO.Request;

public record UserLoginDto(
    string? Email,
    string? Password)
{
    
}