using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoProject.Core.Model.Entity;
using ToDoProject.Core.Repository.Abstract;

namespace ToDoProject.Core.Repository.Concrete;

public abstract class GenericRepository<TContext, TEntity, TId>(TContext context) : IGenericRepository<TEntity, TId>
    where TEntity : EntityBase<TId>, new()
    where TContext : DbContext
{
    protected TContext Context { get; } = context;
    
    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() => await Context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(TId id) => await Context.Set<TEntity>().FindAsync(id);

    public async Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null
            ? await GetAllAsync()
            : await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity) => await Context.Set<TEntity>().AddAsync(entity);

    public Task UpdateAsync(TEntity entity) => Task.FromResult(Context.Set<TEntity>().Update(entity));

    public Task DeleteAsync(TEntity entity) => Task.FromResult(Context.Set<TEntity>().Remove(entity));
}