using Application.DTO.Response;
using Application.Interfaces.Department;
using Domain.Interfaces.Repositories;

namespace Application.Features.Department;

public class ListDepartment : IListDepartment
{
    private readonly IUnitOfWork _unitOfWork;

    public ListDepartment(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DepartmentDto>> ListAllAsync(Guid userId)
    {
        var departments = await _unitOfWork.DepartmentRepository.GetWhereWithUserAsync( x => 
            x.Owner.Id == userId);
        var departmentDtos = departments.Select(department => DepartmentDto.MapFromEntity(department)).ToList();
        return departmentDtos;
    }
}