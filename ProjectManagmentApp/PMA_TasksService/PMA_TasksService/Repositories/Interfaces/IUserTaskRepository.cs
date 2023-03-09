using PMA_TasksService.Models;
using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Repositories.Interfaces
{
    public interface IUserTaskRepository : IBaseAsyncRepository<UserTaskDTO,UserTask>
    {
    }
}
