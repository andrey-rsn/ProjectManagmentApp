using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.UserTaskServices
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userRepository;

        public UserTaskService(IUserTaskRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserTaskDTO> GetById(int id)
        {
            var userTask = await _userRepository.GetByIdAsync(id);

            if(userTask != null) 
            {
                return userTask;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<IEnumerable<UserTaskDTO>> GetAll(int limit)
        {
            var userTasks = await _userRepository.GetAllAsync(limit);

            if (userTasks.Any())
            {
                return userTasks;
            }

            throw new Exception("Данные отсутсвуют");
        }

        public async Task<UserTaskDTO> Update(UserTaskDTO userTask)
        {
            return await _userRepository.UpdateAsync(userTask);
        }

        public async Task<UserTaskDTO> Delete(int userTaskId)
        {
            try
            {
                var userTask = await GetById(userTaskId);

                return await _userRepository.DeleteAsync(userTask);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<UserTaskDTO>> GetAllByUserId(int userId, int limit = 100)
        {
            var UserTasks = await _userRepository.GetAsync(v => v.assignedUserId == userId, limit: limit);

            if (UserTasks.Any())
            {
                return UserTasks;
            }

            throw new Exception("Данные отсутсвуют");
        }

        public async Task<UserTaskDTO> Add(UserTaskDTO userTask)
        {
            return await _userRepository.AddAsync(userTask);
        }

    }
}
