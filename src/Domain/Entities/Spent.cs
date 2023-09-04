namespace Domain.Entities;

public class Spent : BaseEntity
{
   public User? User { get; set; }
   public decimal Amount { get; set; }
   public Sector? Sector { get; set; }
}