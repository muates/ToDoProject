using ToDoProject.Model.Dto.Role.Request;
using ToDoProject.Model.Entity;

namespace ToDoProject.Application.Service.Abstract;

public interface IRoleService
{
    Task<IReadOnlyCollection<Role>> GetAllRoleAsync();
    Task<Role> GetRoleByIdAsync(int id);
    Task<Role> GetRoleByNameAsync(string name);
    Task AddRoleAsync(AddRoleRequest request);
    Task DeleteRoleAsync(int id);
}