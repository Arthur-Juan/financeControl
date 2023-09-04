namespace Domain.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordName { get; set; }
    public List<Role>? Roles { get; set; }
}

public class Role : BaseEntity
{
    public string? Name { get; set; }
}