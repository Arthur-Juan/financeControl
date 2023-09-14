using Application.DTO.Response;

namespace Application.Interfaces.Department;

public interface IListDepartment
{
    Task<List<DepartmentDto>> ListAllAsync(Guid userId);
}