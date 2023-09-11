using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Data.EFCore;

public class DepartmentRepository : IDepartmentRepository, IRepository<Department>
{
    
}