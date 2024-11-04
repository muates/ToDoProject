using ToDoProject.Core.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Abstract;

public interface IRoleRepository : IGenericRepository<Role, int>
{
    Task<Role?> GetRoleByNameAsync(string name);
}