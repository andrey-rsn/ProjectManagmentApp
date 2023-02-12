using Microsoft.Extensions.Caching.Memory;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.UserTaskStatusServices
{
    public class UserTaskStatusService : IUserTaskStatusService
    {
        private readonly IUserTaskStatusRepository _userTaskStatusRepository;
        private readonly IMemoryCache _cache;
        private const string USER_TASK_STATUSES_CAHCE_KEY = "user-task-statuses"; 

        public UserTaskStatusService(IUserTaskStatusRepository userTaskStatusRepository, IMemoryCache cache)
        {
            _userTaskStatusRepository = userTaskStatusRepository;
            _cache = cache;
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
            _cache.TryGetValue(USER_TASK_STATUSES_CAHCE_KEY,out IEnumerable<UserTaskStatusDTO> entities);

            if (entities == null || !entities.Any())
            {
                var userTaskStatuses = await _userTaskStatusRepository.GetAllAsync(limit);

                if(userTaskStatuses.Any())
                {
                    _cache.Set(USER_TASK_STATUSES_CAHCE_KEY,userTaskStatuses);
                    return userTaskStatuses;
                }

                throw new Exception("Данные отсутсвуют");
            }

            return entities;
        }

        public async Task<UserTaskStatusDTO> GetById(int entityId)
        {
            _cache.TryGetValue(USER_TASK_STATUSES_CAHCE_KEY, out IEnumerable<UserTaskStatusDTO> entities);

            if (entities != null && entities.Any())
            {
                var userTaskStatus = entities.FirstOrDefault(obj => obj.userTaskStatusId == entityId);

                if(userTaskStatus != null)
                {
                    return userTaskStatus;
                }
            }
            else
            {
                var userTaskStatuses = await _userTaskStatusRepository.GetAllAsync(100);

                if (userTaskStatuses.Any())
                {
                    _cache.Set(USER_TASK_STATUSES_CAHCE_KEY,userTaskStatuses);

                    var userTaskStatus = userTaskStatuses.FirstOrDefault(obj => obj.userTaskStatusId == entityId);

                    if(userTaskStatus != null)
                    {
                        return userTaskStatus;
                    }
                }
            }

            throw new Exception("Объект не найден");
        }

        public async Task<UserTaskStatusDTO> Update(UserTaskStatusDTO entity)
        {
            return await _userTaskStatusRepository.UpdateAsync(entity);
        }
    }
}
