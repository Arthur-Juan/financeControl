using System.Runtime.Intrinsics.Arm;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.Auth;
using Domain.Errors;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth;

public class RegisterUser : IRegisterUser
{
    private readonly ICryptoService _crypto;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUser(ICryptoService crypto, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _crypto = crypto;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    public async Task<UserLoggedDto> RegisterAsync(UserRegisterDto dto)
    {
        var validator = new Validator();
        var isValid = await validator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            foreach (var error in isValid.Errors)
            {
                throw new BadArgumentException(error.ErrorMessage);
            }

        }

        var userAlreadyExists = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email);
        if (userAlreadyExists is not null)
        {
            throw new BadArgumentException(DomainErrors.User.EmailInUse);
        }

        var hash = _crypto.Encrypt(dto.Password);
        var userEntity = UserRegisterDto.MapToEntity(dto, hash);

        await _unitOfWork.UserRepository.AddAsync(userEntity);
        await _unitOfWork.Commit();
        var token = _tokenService.GenerateToken(userEntity);
        var result = UserLoggedDto.MapFromEntity(userEntity, token);
        return result;
    }

    private class Validator : AbstractValidator<UserRegisterDto>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(DomainErrors.Shared.RequiredFiled("FirstName"));
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(DomainErrors.Shared.RequiredFiled("LastName"));
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(DomainErrors.Shared.RequiredFiled("Email"))
                .EmailAddress()
                .WithMessage(DomainErrors.User.InvalidEmail);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(DomainErrors.Shared.RequiredFiled("Password"))
                .Equal(x => x.ConfirmPassword)
                .WithMessage(DomainErrors.User.PasswordDontMatch);
        }
    }   
}