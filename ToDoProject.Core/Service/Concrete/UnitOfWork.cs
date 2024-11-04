using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoProject.Core.Service.Abstract;

namespace ToDoProject.Core.Service.Concrete;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork where TContext : DbContext
{
    private IDbContextTransaction? _transaction;

    public void BeginTransaction()
    {
        _transaction = context.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
        _transaction?.Dispose();
    }
}