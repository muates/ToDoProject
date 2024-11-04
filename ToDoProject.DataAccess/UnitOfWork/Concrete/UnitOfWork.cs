using ToDoProject.DataAccess.Context;
using ToDoProject.DataAccess.UnitOfWork.Abstract;

namespace ToDoProject.DataAccess.UnitOfWork.Concrete;

public class UnitOfWork(PostgreSqlDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        context.Dispose();
    }
}