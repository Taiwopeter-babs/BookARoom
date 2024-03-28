using System.Linq.Expressions;

namespace BookARoom.Interfaces;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);

    void UpdateTime(T entity);
}