using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Abstract;

public interface IUserRoleRepository
{
    Task AddUserToRoleAsync(UserRole userRole);
    Task RemoveUserFromRoleAsync(string userId, int roleId);
}