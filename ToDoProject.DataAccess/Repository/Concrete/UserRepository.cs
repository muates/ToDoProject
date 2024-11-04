using Microsoft.EntityFrameworkCore;
using ToDoProject.Core.Repository.Concrete;
using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class UserRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, User, int>(context), IUserRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<User?> GetUserByUsernameAsync(string username) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Username == username);

    public async Task<User?> GetUserWithRoleAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> UserExistsAsync(string username) =>
        await _context.Users.AnyAsync(user => user.Username == username);
}