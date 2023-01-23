namespace PMA_WorkTimeService.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int limit);
        Task<T> GetById(int id);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        Task Update(T entity);
        Task DeleteById(int id);
    }
}
