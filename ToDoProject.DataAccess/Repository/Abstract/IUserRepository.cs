using ToDoProject.Core.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Abstract;

public interface IUserRepository : IGenericRepository<User, int>
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserWithRoleAsync(string username);
    Task<bool> UserExistsAsync(string username);
}