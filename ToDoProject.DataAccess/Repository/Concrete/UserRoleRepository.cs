using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class UserRoleRepository(PostgreSqlDbContext context) : IUserRoleRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task AddUserToRoleAsync(UserRole userRole) => await _context.UserRoles.AddAsync(userRole);

    public async Task RemoveUserFromRoleAsync(string userId, int roleId)
    {
        throw new NotImplementedException();
    }
}