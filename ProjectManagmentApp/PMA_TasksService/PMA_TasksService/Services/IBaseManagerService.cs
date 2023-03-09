using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services
{
    public interface IBaseManagerService<T>
    {
        Task<T> GetById(int entityId);

        Task<IEnumerable<T>> GetAll(int limit = 100);

        Task<T> Update(T entity);

        Task<T> Delete(int entityId);

        Task<T> Add(T entity);
    }
}
