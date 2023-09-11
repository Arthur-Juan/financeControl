using Domain.Enum;
using Domain.Errors;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<Department>? Departments { get; set; }
    public List<Department> DepartmentOwner { get; set; }

    public User()
    {
        Departments ??= new List<Department>();
    }

    public User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public bool SetPassword(string? password)
    {
        if (password == null)
        {
            return false;
        }
        Password = password;
        return true;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public void AddNewDepartmentOwned(Department department)
    {
        DepartmentOwner ??= new List<Department>();
        Departments?.Add(department);
    }
}

public class Role : BaseEntity
{
    public string? Name { get; set; }
    public RolesEnum RolePart { get; set; }
}