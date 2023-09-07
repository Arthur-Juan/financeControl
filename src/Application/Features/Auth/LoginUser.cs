using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.Auth;
using Domain.Errors;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Auth;

public class LoginUser : ILoginUser
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ICryptoService _cryptoService;
    public LoginUser(IUserRepository userRepository, ITokenService tokenService, ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _cryptoService = cryptoService;
    }

    public async Task<UserLoggedDto> LoginAsync(UserLoginDto dto)
    {
        var validator = new Validation();
        var isValid = validator.Validate(dto);
        
        if (!isValid.IsValid)
        {
         var error =   isValid.Errors.FirstOrDefault();
         throw new BadArgumentException(error.ErrorMessage);
        }

        var userExists = await _userRepository.GetUserByEmailAsync(dto.Email);
        
        if (userExists == null)
        {
            throw new ForbiddenException(DomainErrors.User.InvalidCredentials);
        }

        var validPassword = _cryptoService.ValidateHash(dto.Password, userExists.Password);
        if (!validPassword)
        {
            throw new ForbiddenException(DomainErrors.User.InvalidCredentials);
        }

        var token = _tokenService.GenerateToken(userExists);

        var result = UserLoggedDto.MapFromEntity(userExists, token);

        return result;
    }
}

public class Validation : AbstractValidator<UserLoginDto>
{
    public Validation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(DomainErrors.Shared.RequiredFiled("email"))
            .EmailAddress().WithMessage(DomainErrors.User.InvalidEmail);
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(DomainErrors.Shared.RequiredFiled("password"));
    }
}