using Domain.Interfaces.Repositories;
using Infra.Data.EFCore;

namespace Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    #region repositories

    private IUserRepository _userRepository;
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    
    #endregion


    public Task<bool> Commit()
    {
        throw new NotImplementedException();
    }
}