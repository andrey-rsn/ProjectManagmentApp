using PMA_TasksService.Models.DTOs;

namespace PMA_TasksService.Services.UserTask
{
    public interface IUserTaskService
    {
        Task<UserTaskDTO> GetById(int id);

        Task<IEnumerable<UserTaskDTO>> GetAll(int limit = 100);

        Task<UserTaskDTO> Update(UserTaskDTO userTask);

        Task<UserTaskDTO> Delete(int userTaskId);

        Task<IEnumerable<UserTaskDTO>> GetAllByUserId(int userId, int limit = 100);

        Task<UserTaskDTO> Add(UserTaskDTO userTask);

    }
}
