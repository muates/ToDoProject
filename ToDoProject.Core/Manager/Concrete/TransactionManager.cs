using ToDoProject.Core.Manager.Abstract;
using ToDoProject.Core.Service.Abstract;

namespace ToDoProject.Core.Manager.Concrete;

public class TransactionManager(IUnitOfWork unitOfWork) : ITransactionManager
{
    public async Task<T> ExecuteInTransaction<T>(Func<Task<T>> action)
    {
        unitOfWork.BeginTransaction();
        try
        {
            var result = await action();
            await unitOfWork.CommitAsync();
            return result;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}