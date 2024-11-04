namespace ToDoProject.Core.Service.Abstract;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
    Task<int> SaveChangesAsync();
}