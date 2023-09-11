using System.Reflection;
using System.Runtime.InteropServices;
using Application.DTO.Request;
using Application.Features.Department;
using Application.Interfaces.Department;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace tests.Features.Department;

public class CreateDepartmentTest
{
 private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

 private ICreateDepartment MakeSut(IUnitOfWork unitOfWork)
 {
  return new CreateDepartment(unitOfWork);
 }

 private User GetFakeUser()
 {
     return new User(
         "Test",
         "Test",
         "testEmail@email.com",
         "pass"
         );
 }

 [Theory]
 [InlineData("")]
 public async Task Test_ThrowErrorIfInvalidData(string name)
 {
     var sut = MakeSut(_unitOfWork);

     var dto = new DepartmentCreateDto(name);
     var user = GetFakeUser();
     await Assert.ThrowsAsync<BadArgumentException>(async () =>
     {
         await sut.CreateAsync(dto, user.Id);
     } );
 }

 [Fact]
 public async Task Test_ThrowErrorIfUserNotFound()
 {
     var dto = new DepartmentCreateDto("test");
     var userId = Guid.NewGuid();

     _unitOfWork.UserRepository.GetByIdAsync(userId).ReturnsNull();
     var sut = MakeSut(_unitOfWork);

     await Assert.ThrowsAsync<NotFoundException>(async () =>
     {
         await sut.CreateAsync(dto, userId);

     });
 }

 [Fact]
 public async Task Test_IfDepartmentHaveADefaultSector()
 {
     var dto = new DepartmentCreateDto("test");
     var user = GetFakeUser();
     
     _unitOfWork.UserRepository.GetByIdAsync(user.Id).Returns(user);
     var sut = MakeSut(_unitOfWork);
     var result = await sut.CreateAsync(dto, user.Id);
     Assert.True(result.Sectors?.Any());
     Assert.True(result.Sectors?.FirstOrDefault()!.Name == "All");
 }

 [Fact]
 public async Task Test_EnsureTheOwnerIsTheRequestMake()
 {
     var dto = new DepartmentCreateDto("test");
     var user = GetFakeUser();
     
     _unitOfWork.UserRepository.GetByIdAsync(user.Id).Returns(user);
     var sut = MakeSut(_unitOfWork);

     
     var result = await sut.CreateAsync(dto, user.Id);
     Assert.True(result.UserId == user.Id);
 }
 
}