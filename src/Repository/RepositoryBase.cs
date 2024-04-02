using System.Linq.Expressions;
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookARoom.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected BookARoomContext _bookARoomContext;

    public RepositoryBase(BookARoomContext bookARoomContext) =>
        _bookARoomContext = bookARoomContext;

    public async Task AddItems(List<T> items) => await _bookARoomContext.AddRangeAsync(items);

    /// <summary>
    /// Add an entity to the database
    /// </summary>
    /// <param name="entity"></param>
    public void Create(T entity) => _bookARoomContext.Set<T>().Add(entity);

    /// <summary>
    /// Remove an entity from the database
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(T entity) => _bookARoomContext.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges)
    {
        if (!trackChanges)
            return _bookARoomContext.Set<T>().AsNoTracking();

        return _bookARoomContext.Set<T>();
    }

    /// <summary>
    /// Finds an entity by a condition given by the expression. Also includes condition
    /// to load the entities related.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="trackChanges">A boolean to check tracking entity in the storage</param>
    /// <returns></returns>
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges)
    {
        if (!trackChanges)
            return _bookARoomContext.Set<T>()
            .Where(expression)
            .AsNoTracking();

        return _bookARoomContext.Set<T>().Where(expression);
    }

    public void Update(T entity)
    {
        _bookARoomContext.Set<T>().Update(entity);
        // UpdateTime(entity);
    }


    /// <summary>
    /// Update the updatedAt field of the modified entity
    /// </summary>
    /// <param name="entity"></param>
    public void UpdateTime(T entity)
    {
        var entityEntry = _bookARoomContext.Set<T>().Entry(entity);

        if (entityEntry.State == EntityState.Modified)
        {
            // update the updatedAt time
            _bookARoomContext.Set<T>().Entry(entity)
                .Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
    }
}