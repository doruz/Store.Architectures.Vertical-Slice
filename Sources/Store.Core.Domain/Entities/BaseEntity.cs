namespace Store.Core.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString().ToLower();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? DeletedAt { get; set; }
    
    public bool IsDeleted() => DeletedAt is not null;
    public virtual void MarkAsDeleted() => DeletedAt = DateTime.UtcNow;
}