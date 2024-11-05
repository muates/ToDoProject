namespace ToDoProject.Core.Service.Abstract;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}