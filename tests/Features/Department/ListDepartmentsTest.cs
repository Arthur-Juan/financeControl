using System.Linq.Expressions;
using Application.Features.Department;
using Application.Interfaces.Department;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using NSubstitute;

namespace tests.Features.Department;

public class ListDepartmentsTest
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private  IListDepartment _sut;

    private IListDepartment MakeSut(IUnitOfWork unitOfWork)
    {
        return new ListDepartment(unitOfWork);
    }

    [Fact]
    public async Task Test_ThrowExceptionIfNoDepartmentToUser()
    {
        var userId = Guid.NewGuid();
        _unitOfWork.DepartmentRepository.GetWhereWithUserAsync(x =>
            x.Owner.Id == userId).ReturnsForAnyArgs(new List<Domain.Entities.Department>());

        _sut = MakeSut(_unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sut.ListAllAsync(userId);
        });
    }

}