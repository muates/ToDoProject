namespace ToDoProject.Application.Service.Abstract;

public interface IUserRoleService
{
    Task AddUserToRoleAsync(int userId, int roleId);
}