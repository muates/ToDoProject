using Microsoft.EntityFrameworkCore;
using ToDoProject.Core.Service.Abstract;

namespace ToDoProject.Core.Service.Concrete;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork
    where TContext : DbContext
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