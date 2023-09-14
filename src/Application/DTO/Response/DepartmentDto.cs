using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Application.DTO.Response
{
    public record DepartmentDto(
        Guid Id,
        string? Name,
        List<SectorDto>? Sectors,
        Guid? UserId
    )
    {
        public static DepartmentDto MapFromEntity(Department entity)
        {
            Guid? ownerId = entity?.Owner?.Id;

            List<SectorDto>? sectors = entity?.Sectors?.Select(sector => SectorDto.MapFromEntity(sector)).ToList();

            return new DepartmentDto(
                entity.Id,
                entity?.Name,
                sectors,
                ownerId
            );
        }
    }

    public record UserDto(
        Guid Id
    );

    public record SectorDto(
        Guid Id,
        string? Name
    )
    {
        public static SectorDto MapFromEntity(Sector entity)
        {
            return new SectorDto(entity.Id, entity?.Name);
        }
    }
}