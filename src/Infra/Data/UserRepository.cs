using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces.Repositories;
using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync( x => x!.Email == email);
    }

}