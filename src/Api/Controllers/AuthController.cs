using Application.DTO.Request;
using Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUser _registerUser;
    private readonly ILoginUser _loginUser;

    public AuthController(IRegisterUser registerUser, ILoginUser loginUser)
    {
        _registerUser = registerUser;
        _loginUser = loginUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _registerUser.RegisterAsync(dto);
        return Created("/", result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var result = await _loginUser.LoginAsync(dto);
        return Ok(result);
    }
}