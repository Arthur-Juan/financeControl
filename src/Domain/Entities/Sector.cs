namespace Domain.Entities;

public class Sector : BaseEntity
{
    public string? Name { get; set; }
    public Department? Department { get; set; }
    public List<Spent>? Spents { get; set; }

    public decimal GetTotalSpent()
    {
        return Spents?.Sum(spent => spent.Amount) ?? 0;
    }

    public Sector()
    {
        Spents ??= new List<Spent>();
    }
}