using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.Department;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Department;

public class CreateDepartment : ICreateDepartment
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartment(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto, Guid userId)
    {
        var validator = new Validator();
        var isValid = await validator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var error = isValid.Errors.FirstOrDefault();
            throw new BadArgumentException(error.ErrorMessage);
        }
        
        var entity = DepartmentCreateDto.MapToEntity(dto);
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        entity.Slugify();

        var departmentAlreadyExists = await _unitOfWork.DepartmentRepository.GetBySlugAsync(entity.Slug);
        if (departmentAlreadyExists != null)
        {
            throw new BadArgumentException(DomainErrors.Department.AlreadyExists);
        }
        
        if (!entity.SetOwner(user))
        {
            throw new NotFoundException(DomainErrors.Department.UserNotFound);
        }
        
        user?.AddNewDepartmentOwned(entity);
        await _unitOfWork.DepartmentRepository.AddAsync(entity);

        _unitOfWork.UserRepository.Update(user);
        
        await _unitOfWork.Commit();
        var result = DepartmentDto.MapFromEntity(entity);
        return result;
    }

}



internal class Validator : AbstractValidator<DepartmentCreateDto>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(DomainErrors.Shared.RequiredFiled("Name"))
            .MaximumLength(60).WithMessage("Name is too long");

    }
}