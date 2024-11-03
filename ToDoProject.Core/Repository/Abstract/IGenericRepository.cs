using System.Linq.Expressions;
using ToDoProject.Core.Model.Entity;

namespace ToDoProject.Core.Repository.Abstract;

public interface IGenericRepository<TEntity, in TId> where TEntity : EntityBase<TId>, new()
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}