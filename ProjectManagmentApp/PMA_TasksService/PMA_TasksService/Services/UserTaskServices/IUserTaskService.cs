using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.UserTaskServices
{
    public interface IUserTaskService : IBaseManagerService<UserTaskDTO>
    {
        Task<IEnumerable<UserTaskDTO>> GetAllByUserId(int userId, int limit = 100);
    }
}
