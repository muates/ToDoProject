using ToDoProject.Core.Repository.Concrete;
using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Entity;

namespace ToDoProject.DataAccess.Repository.Concrete;

public class CategoryRepository(PostgreSqlDbContext context)
    : GenericRepository<PostgreSqlDbContext, Category, int>(context), ICategoryRepository
{
    private readonly PostgreSqlDbContext _context = context;
    
}