using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync( x => x!.Email == email);
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

    }
}