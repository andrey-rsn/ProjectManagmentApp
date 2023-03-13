using System.Linq.Expressions;

namespace PMA_ProjectsService.Repositories.Interfaces
{
    public interface IBaseAsyncRepository<T, U>
    {
        Task<IReadOnlyList<T>> GetAllAsync(int limit);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate = null,
                                        Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<U, bool>> predicate = null,
                                       Func<IQueryable<U>, IOrderedQueryable<U>> orderBy = null,
                                       List<Expression<Func<U, object>>> includes = null,
                                       bool disableTracking = true, int limit = 100);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
