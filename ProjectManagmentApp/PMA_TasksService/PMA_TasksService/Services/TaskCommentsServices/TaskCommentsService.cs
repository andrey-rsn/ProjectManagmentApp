using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.TaskCommentsServices
{
    public class TaskCommentsService : ITaskCommentsService
    {
        private readonly ITaskCommentsRepository _taskCommentsRepository;

        public TaskCommentsService(ITaskCommentsRepository taskCommentsRepository)
        {
            _taskCommentsRepository = taskCommentsRepository;
        }

        public async Task<TaskCommentsDTO> Add(TaskCommentsDTO entity)
        {
            return await _taskCommentsRepository.AddAsync(entity);
        }

        public async Task<TaskCommentsDTO> Delete(int entityId)
        {
            var taskComment = await GetById(entityId);

            if(taskComment != null)
            {
                return taskComment;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<IEnumerable<TaskCommentsDTO>> GetAll(int limit = 100)
        {
            var taskComments = await _taskCommentsRepository.GetAllAsync(limit);

            if (taskComments.Any())
            {
                return taskComments;
            }

            throw new Exception("Данные отсутсвуют");
        }

        public async Task<TaskCommentsDTO> GetById(int entityId)
        {
            var taskComment = await _taskCommentsRepository.GetByIdAsync(entityId);

            if(taskComment != null )
            {
                return taskComment;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<TaskCommentsDTO> Update(TaskCommentsDTO entity)
        {
            return await _taskCommentsRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<TaskCommentsDTO>> GetByTaskId(int taskId)
        {
            var taskComment = await _taskCommentsRepository.GetAsync(obj => obj.taskId == taskId);

            if (taskComment != null)
            {
                return taskComment;
            }

            throw new Exception("Данные отсутсвуют");
        }
    }
}
