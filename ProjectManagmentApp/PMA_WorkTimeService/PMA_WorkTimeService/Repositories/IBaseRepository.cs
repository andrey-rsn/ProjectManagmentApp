namespace PMA_WorkTimeService.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        Task Update(T entity);
        Task DeleteById(int id);
    }
}
