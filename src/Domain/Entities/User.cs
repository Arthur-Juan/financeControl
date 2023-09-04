using Domain.Enum;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<Department>? Departments { get; set; }
}

public class Role : BaseEntity
{
    public string? Name { get; set; }
    public RolesEnum RolePart { get; set; }
}