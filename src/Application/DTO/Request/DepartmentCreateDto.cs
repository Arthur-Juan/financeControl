using Domain.Entities;

namespace Application.DTO.Request;

public record DepartmentCreateDto(
    string Name
)
{
    public static Department MapToEntity(DepartmentCreateDto dto)
    {
        return new Department(dto.Name);
    }
}