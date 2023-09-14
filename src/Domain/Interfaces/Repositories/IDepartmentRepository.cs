using System.Linq.Expressions;
using Domain.Entities;
using Domain.Errors;

namespace Domain.Interfaces.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetBySlugAsync(string slug);
    Task<List<Department>> GetWhereWithUserAsync(Expression<Func<Department, bool>> predicate);
}