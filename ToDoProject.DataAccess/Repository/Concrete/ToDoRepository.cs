using ToDoProject.Core.Repository.Concrete;
using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class ToDoRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, ToDo, Guid>(context), IToDoRepository
{
    private readonly PostgreSqlDbContext _context = context;
    
}