using Application.DTO.Request;
using Application.Features.Auth;
using Application.Interfaces.Auth;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace tests.Features.Auth;

public class LoginTest
{
    private readonly ICryptoService _cryptoService = Substitute.For<ICryptoService>();
    private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    private static ILoginUser MakeSut(ICryptoService cryptoService, ITokenService tokenService, IUserRepository userRepository)
    {
        return new LoginUser(userRepository, tokenService, cryptoService);
    }
    
    [Theory]
    [InlineData("invalidEmail","pass")]
    [InlineData("", "pass")]
    [InlineData("email@email.com", "")]
    public async Task Test_ThrowErrorWithInvalidData(string email, string password)
    {
        var dto = new UserLoginDto(email, password);
        var sut = new LoginUser(_userRepository,_tokenService,_cryptoService);

        await Assert.ThrowsAsync<BadArgumentException>(async () =>
        {
            await sut.LoginAsync(dto);
        });
    }

    [Fact]
    public async Task Test_ThrowErrorsIfUserNotFound()
    {
        var dto = new UserLoginDto("anyemail@email.com", "pass");

        _userRepository.GetUserByEmailAsync(dto.Email).ReturnsNullForAnyArgs();
        var sut = MakeSut(_cryptoService, _tokenService, _userRepository);

        await Assert.ThrowsAsync<ForbiddenException>(
            async () => await sut.LoginAsync(dto)
            );
            
    }

    [Fact]
    public async Task Test_ThrowErrorIfPasswordIsInvalid()
    {
        var dto = new UserLoginDto("anyemail@email.com", "notMyPass");
        _userRepository.GetUserByEmailAsync(dto.Email)
            .Returns(new User("FirsName", "LastName", "anyemail@email.com", "hashedPass"));

        _cryptoService.ValidateHash(dto.Password, "hashedPass").Returns(false);

        var sut = MakeSut(_cryptoService, _tokenService, _userRepository);

        await Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await sut.LoginAsync(dto);
        });
    }

    [Fact]
    public async Task Test_IfValidCredentialsReturnToken()
    {
        var dto = new UserLoginDto("anyemail@email.com", "notMyPass");
        _userRepository.GetUserByEmailAsync(dto.Email)
            .Returns(new User("FirsName", "LastName", "anyemail@email.com", "hashedPass"));
        
        _cryptoService.ValidateHash(dto.Password, "hashedPass").Returns(true);
        _tokenService.GenerateToken(new User("FirsName", "LastName", "anyemail@email.com", "hashedPass"))
            .ReturnsForAnyArgs("SomeToken");

        var sut = MakeSut(_cryptoService, _tokenService, _userRepository);
        var result = await sut.LoginAsync(dto);
        Assert.Equal(result.Token, "SomeToken");
    }
}