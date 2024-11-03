namespace ToDoProject.DataAccess.Repository.Abstract;

public interface IUserRoleRepository
{
    Task AddUserToRoleAsync(string userId, int roleId);
    Task RemoveUserFromRoleAsync(string userId, int roleId);
}