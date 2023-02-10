using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.UserTaskStatusServices
{
    public class UserTaskStatusService : IUserTaskStatusService
    {
        private readonly IUserTaskStatusRepository _userTaskStatusRepository;

        public UserTaskStatusService(IUserTaskStatusRepository userTaskStatusRepository)
        {
            _userTaskStatusRepository = userTaskStatusRepository;
        }

        public async Task<UserTaskStatusDTO> Add(UserTaskStatusDTO entity)
        {
            return await _userTaskStatusRepository.AddAsync(entity);
        }

        public async Task<UserTaskStatusDTO> Delete(int entityId)
        {
            try
            {
                var userTaskStatus = await GetById(entityId);

                return userTaskStatus;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<UserTaskStatusDTO>> GetAll(int limit = 100)
        {
            var userTaskStatuses = await _userTaskStatusRepository.GetAllAsync(limit);

            if(userTaskStatuses.Any())
            {
                return userTaskStatuses;
            }

            throw new Exception("Данные отсутсвуют");
        }

        public async Task<UserTaskStatusDTO> GetById(int entityId)
        {
            var userTaskStatus = await _userTaskStatusRepository.GetByIdAsync(entityId);

            if(userTaskStatus != null)
            {
                return userTaskStatus;
            }

            throw new Exception("Объект не найден");
        }

        public async Task<UserTaskStatusDTO> Update(UserTaskStatusDTO entity)
        {
            return await _userTaskStatusRepository.UpdateAsync(entity);
        }
    }
}
