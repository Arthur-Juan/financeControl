using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infra.Services;

public class JwtAdapter : ITokenService
{
    private readonly IJwtConfiguration _configuration;
    public JwtAdapter(IJwtConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.Key!));

        var signinCredentials = new SigningCredentials(
            secretKey,
            SecurityAlgorithms.HmacSha256
            );

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user?.Email.ToString()),
            new Claim(ClaimTypes.Name, user.GetFullName())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience:_configuration.Audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: signinCredentials,
            claims: claims
            );

        var handler = new JwtSecurityTokenHandler();
        var tokenContent = handler.WriteToken(token);
        return tokenContent;
        
    }
}

public interface IJwtConfiguration
{
    string? Key { get; }
    string? Issuer { get; }
    string? Audience { get; }
}

public class JwtConfiguration : IJwtConfiguration
{
    public JwtConfiguration(string? key, string? audience, string? issuer)
    {
        Key = key;
        Audience = audience;
        Issuer = issuer;
    }

    public string? Key { get; }
    public string? Issuer { get; }
    public string? Audience { get; }
}