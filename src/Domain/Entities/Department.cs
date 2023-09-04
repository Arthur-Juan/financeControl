namespace Domain.Entities;

public class Department : BaseEntity
{
    public string? Name;
    public List<Sector>? Sectors { get; private set; }
    public List<User>? Users { get; set; }

    public Department()
    {
        Sectors ??= new List<Sector>
        {
            new Sector
            {
                Name = "All"
            }
        };
    }
}