using ToDoProject.Core.Model.Entity;

namespace ToDoProject.Model.Entity;

public sealed class User : EntityBase<int>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string SecurityStamp { get; set; }
    public DateTime LastLoginDate { get; set; }
    
    public List<UserRole> UserRoles { get; set; } = [];
}