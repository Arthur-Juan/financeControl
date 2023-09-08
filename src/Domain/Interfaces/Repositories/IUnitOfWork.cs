namespace Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    Task<bool> Commit();
}