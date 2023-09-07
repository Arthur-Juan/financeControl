namespace Domain.Interfaces;

public interface ICryptoService
{
    string Encrypt(string text);
    bool ValidateHash(string text, string hash);
}