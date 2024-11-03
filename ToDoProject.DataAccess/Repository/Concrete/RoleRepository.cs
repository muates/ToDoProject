using ToDoProject.Core.Repository.Concrete;
using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class RoleRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, Role, int>(context), IRoleRepository
{
    private readonly PostgreSqlDbContext _context = context;
    
}