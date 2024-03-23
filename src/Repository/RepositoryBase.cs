using System.Linq.Expressions;
using BookARoom.Data;
using BookARoom.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected BookARoomContext BookARoomContext;

    public RepositoryBase(BookARoomContext bookARoomContext) =>
        BookARoomContext = bookARoomContext;

    /// <summary>
    /// Add an entity to the database
    /// </summary>
    /// <param name="entity"></param>
    public void Create(T entity) => BookARoomContext.Set<T>().Add(entity);

    /// <summary>
    /// Remove an entity from the database
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(T entity) => BookARoomContext.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges)
    {
        if (!trackChanges)
            return BookARoomContext.Set<T>().AsNoTracking();

        return BookARoomContext.Set<T>();
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
            return BookARoomContext.Set<T>()
            .Where(expression)
            .AsNoTracking();

        return BookARoomContext.Set<T>().Where(expression);
    }

    public void Update(T entity) => BookARoomContext.Set<T>().Update(entity);
}