namespace Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity(){}
    
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset DeletedAt { get; set; }

}