namespace Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(){}
    
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}