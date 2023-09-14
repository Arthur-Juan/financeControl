using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Department?> GetBySlugAsync(string slug)
    {
        return await _context.Departments.FirstOrDefaultAsync( x => x.Slug == slug);
    }

    public async Task<List<Department>> GetWhereWithUserAsync(Expression<Func<Department, bool>> predicate)
    {
        return await _context.Set<Department>()
            .Where(predicate)
            .Include(x => x.Owner)
            .ToListAsync();
    }
}