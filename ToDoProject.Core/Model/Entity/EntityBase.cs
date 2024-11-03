namespace ToDoProject.Core.Model.Entity;

public abstract class EntityBase<TId>
{
    public TId Id { get; private set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}