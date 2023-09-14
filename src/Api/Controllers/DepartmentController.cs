using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.Department;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly ICreateDepartment _createDepartment;
    private readonly IListDepartment _listDepartment;
    public DepartmentController(ICreateDepartment createDepartment, IUnitOfWork unitOfWork, IListDepartment listDepartment)
    {
        _createDepartment = createDepartment;
        _listDepartment = listDepartment;
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

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        var token = new JwtSecurityTokenHandler().ReadJwtToken(authHeader.Split(' ')[1]);

        // Get the value of the "sub" claim
        var subClaim = token.Claims.FirstOrDefault(c => c.Type == "sub");
        var subValue = subClaim?.Value;
        var userId = Guid.Parse(subValue!);

        var dtos = await _listDepartment.ListAllAsync(userId);
        return Ok(dtos);
    }

    // [HttpGet("{id}")]
    // public Task<IActionResult> Get([FromRoute] Guid guid)
    // {
    //     
    // }
}