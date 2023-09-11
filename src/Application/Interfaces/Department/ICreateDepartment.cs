using Application.DTO.Request;
using Application.DTO.Response;

namespace Application.Interfaces.Department;

public interface ICreateDepartment
{
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto, Guid userId);
}