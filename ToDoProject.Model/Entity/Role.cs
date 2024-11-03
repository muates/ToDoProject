using ToDoProject.Core.Model.Entity;

namespace ToDoProject.Model.Entity;

public sealed class Role : EntityBase<int>
{
    public string Name { get; set; }
    
    public List<UserRole> UserRoles { get; set; } = [];
}