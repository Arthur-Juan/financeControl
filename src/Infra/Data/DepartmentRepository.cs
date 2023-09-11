using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.EFCore;

namespace Infra.Data;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}