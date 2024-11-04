namespace ToDoProject.Core.Manager.Abstract;

public interface ITransactionManager
{
    Task<T> ExecuteInTransaction<T>(Func<Task<T>> action);
}