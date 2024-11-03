using ToDoProject.Core.Model.Entity;
using ToDoProject.Model.Enum;

namespace ToDoProject.Model.Entity;

public sealed class ToDo : EntityBase<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public bool Completed { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}