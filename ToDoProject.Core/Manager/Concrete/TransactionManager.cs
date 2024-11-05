using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoProject.Core.Manager.Abstract;
using ToDoProject.Core.Service.Abstract;

namespace ToDoProject.Core.Manager.Concrete;

public class TransactionManager<TContext>(IUnitOfWork unitOfWork, TContext context) : ITransactionManager where TContext : DbContext
{
    private IDbContextTransaction? _transaction;

    private async Task BeginTransactionAsync()
    {
        _transaction ??= await context.Database.BeginTransactionAsync();
    }

    public async Task<T> ExecuteInTransaction<T>(Func<Task<T>> action)
    {
        await BeginTransactionAsync();
        try
        {
            var result = await action();
            await unitOfWork.SaveChangesAsync();
            await _transaction?.CommitAsync();
            return result;
        }
        catch
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            throw;
        }
        finally
        {
            if (_transaction != null) await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}