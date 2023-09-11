namespace Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
    Task<bool> Commit();
}