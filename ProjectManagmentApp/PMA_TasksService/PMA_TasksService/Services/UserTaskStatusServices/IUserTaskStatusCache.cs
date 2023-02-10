using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.UserTaskStatusServices
{
    public interface IUserTaskStatusCache
    {
        Task<IEnumerable<UserTaskStatusDTO>> GetEntities(int limit = 5);

    }
}
