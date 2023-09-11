using Domain.Entities;

namespace Application.DTO.Response;

public record DepartmentDto(
    Guid Id,
    string? Name,
    List<SectorDto>? Sectors,
    Guid? UserId
)
{
    public static DepartmentDto MapFromEntity(Department entity)
    {
        var sectors = (from sector in entity?.Sectors select SectorDto.MapFromEntity(sector)).ToList();
        return new DepartmentDto(
            entity.Id,
            entity?.Name,
            sectors,
            entity?.Owner.Id);
    }
    
}

public record UserDto(
    Guid Id);

public record SectorDto(
    Guid Id,
    string? Name
)
{
    public static SectorDto MapFromEntity(Sector entity)
    {
        return new SectorDto(entity.Id ,entity?.Name);
    }
}