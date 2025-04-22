namespace Domain.Models;

// Parent for all Domain models
public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; } // PK
}