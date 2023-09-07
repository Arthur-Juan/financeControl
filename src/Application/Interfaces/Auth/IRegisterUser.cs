using Application.DTO.Request;
using Application.DTO.Response;

namespace Application.Interfaces.Auth;

public interface IRegisterUser
{
    Task<UserLoggedDto> RegisterAsync(UserRegisterDto dto);
}