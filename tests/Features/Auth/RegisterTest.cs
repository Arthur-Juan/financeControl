using System.Runtime.InteropServices;
using Application.DTO.Request;
using Application.Features.Auth;
using Application.Interfaces.Auth;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Infra.Data.EFCore;
using Infra.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using AutoFixture;
using Domain.Interfaces.Repositories;
using NSubstitute.ReturnsExtensions;

namespace tests.Features.Auth;

public class RegisterTest 
{

    public RegisterTest()
    {
        _sut = new RegisterUser(_crypto, _tokenService, _userRepository);
    }
    
    private readonly IRegisterUser _sut;
    private readonly ICryptoService _crypto = new BcryptAdapter();
    private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        

    [Theory]
    [InlineData("", "test", "teste@email.com", "pass", "pass")]
    [InlineData("test", "", "teste@email.com", "pass", "pass")]
    [InlineData("test", "test", "test", "pass", "pass")]
    [InlineData("test", "test", "", "pass", "pass")]
    [InlineData("test", "test", "teste@email.com", "", "pass")]
    [InlineData("test", "test", "teste@email.com", "pass", "")]
    public async Task Test_InvalidData(string? firstName,
        string? lastname,
        string? email,
        string? password,
        string? confirmPassword)
    {
        var dto = new UserRegisterDto(firstName, lastname, email, password, confirmPassword);
        await Assert.ThrowsAsync<BadArgumentException>(async () => await _sut.RegisterAsync(dto));
    }

    [Theory]
    [InlineData("pass1", "pass2")]
    public async Task Test_UserCanNotLoginIfPasswordDontMatch(string? pass1, string? pass2)
    {
        var dto = new UserRegisterDto(
            "test",
            "test",
            "test@email.com",
            pass1,
            pass2
            );
        await Assert.ThrowsAsync<BadArgumentException>(async () => await _sut.RegisterAsync(dto));
    }

    [Theory]
    [InlineData("email")]
    [InlineData("InvalidEmailFormat")]
    public async Task Test_UserCanNotLoginWithInvalidEmail(string email)
    {
        var dto = new UserRegisterDto(
            "test",
            "test",
            email,
            "pass1",
            "pass1"
            );

        await Assert.ThrowsAsync<BadArgumentException>(
            async () => await _sut.RegisterAsync(dto)
            );
    }

    [Fact]
    public async Task Test_UserCanNotLoginIfEmailIsInUse()
    {
        var dto = new UserRegisterDto(
            "test",
            "test",
            "exists@email.com",
            "pass1",
            "pass1"
            );

        var user = new User(
            firstName: "Test",
            lastName: "test",
            email: "exists@email.com",
            password: "myPass"
            );
        var fakeRepo = Substitute.For<IUserRepository>();
        fakeRepo.GetUserByEmailAsync(dto.Email).Returns(user);
        var sut = new RegisterUser(_crypto, _tokenService, fakeRepo);
        
        await Assert.ThrowsAsync<BadArgumentException>(async () =>
        {
            await sut.RegisterAsync(dto: dto);
        });
    }

    [Fact]
    public async Task Test_IfTokenWasCreated()
    {
        var dto = new UserRegisterDto(
            "test",
            "test",
            "test@email.com",
            "pass1",
            "pass1"
        );

        var fakeToken = Substitute.For<ITokenService>();
        var entity = UserRegisterDto.MapToEntity(dto, "hash");
        
        fakeToken.GenerateToken(entity)
            .ReturnsForAnyArgs("tokenTest");

        var fakeCrypto = Substitute.For<ICryptoService>();
        fakeCrypto.Encrypt(dto.Password).Returns("hash");

        var fakeRepo = Substitute.For<IUserRepository>();
       
        fakeRepo
            .GetUserByEmailAsync(dto.Email)
            .ReturnsNull();

        var sut = new RegisterUser(fakeCrypto, fakeToken, fakeRepo);
        var result = await sut.RegisterAsync(dto: dto);
        Assert.Equal("tokenTest", result.Token);
    }
    
    
}