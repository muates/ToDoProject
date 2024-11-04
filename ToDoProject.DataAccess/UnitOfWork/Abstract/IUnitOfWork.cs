namespace ToDoProject.DataAccess.UnitOfWork.Abstract;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}