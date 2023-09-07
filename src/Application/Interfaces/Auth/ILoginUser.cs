using Application.DTO.Request;
using Application.DTO.Response;

namespace Application.Interfaces.Auth;

public interface ILoginUser
{
    Task<UserLoggedDto> LoginAsync(UserLoginDto dto);
}