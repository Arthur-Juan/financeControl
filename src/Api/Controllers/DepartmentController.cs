using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTO.Request;
using Application.Interfaces.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly ICreateDepartment _createDepartment;

    public DepartmentController(ICreateDepartment createDepartment)
    {
        _createDepartment = createDepartment;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        var token = new JwtSecurityTokenHandler().ReadJwtToken(authHeader.Split(' ')[1]);

        // Get the value of the "sub" claim
        var subClaim = token.Claims.FirstOrDefault(c => c.Type == "sub");
        var subValue = subClaim?.Value;
        var guid = Guid.Parse(subValue!);

        
        var result = await _createDepartment.CreateAsync(dto, guid);
        return Created($"/departments/{result.Id}", result);
    }
}