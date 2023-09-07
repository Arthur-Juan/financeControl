using Application.DTO.Request;
using Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUser _registerUser;

    public AuthController(IRegisterUser registerUser)
    {
        _registerUser = registerUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _registerUser.RegisterAsync(dto);
        return Created("/", result);
    }
}