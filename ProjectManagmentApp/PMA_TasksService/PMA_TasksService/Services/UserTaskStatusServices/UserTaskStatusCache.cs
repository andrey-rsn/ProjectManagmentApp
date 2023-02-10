using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Repositories.Interfaces;

namespace PMA_TasksService.Services.UserTaskStatusServices
{
    public class UserTaskStatusCache : IUserTaskStatusCache
    {
        private IEnumerable<UserTaskStatusDTO> _userTaskStatuses;
        private readonly IUserTaskStatusRepository _userTaskStatusesRepository;

        public UserTaskStatusCache(IUserTaskStatusRepository userTaskStatusesRepository)
        {
            _userTaskStatusesRepository = userTaskStatusesRepository;

            _userTaskStatuses= new List<UserTaskStatusDTO>();
        }

        public async Task<IEnumerable<UserTaskStatusDTO>> GetEntities(int limit = 5)
        {
            if(_userTaskStatuses.Any())
            {
                return _userTaskStatuses;
            }

            _userTaskStatuses = await _userTaskStatusesRepository.GetAllAsync(limit);

            return _userTaskStatuses;
        }
    }
}
