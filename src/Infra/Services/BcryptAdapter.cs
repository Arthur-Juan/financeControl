using Domain.Interfaces;

namespace Infra.Services;

public class BcryptAdapter : ICryptoService
{
    public string Encrypt(string text)
    {
        return BCrypt.Net.BCrypt.HashPassword(text);
    }
}