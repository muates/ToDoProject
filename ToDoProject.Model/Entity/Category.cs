using ToDoProject.Core.Model.Entity;

namespace ToDoProject.Model.Entity;

public sealed class Category : EntityBase<int>
{
    public string Name { get; set; }
    
    public List<ToDo> ToDos { get; set; } = [];
}