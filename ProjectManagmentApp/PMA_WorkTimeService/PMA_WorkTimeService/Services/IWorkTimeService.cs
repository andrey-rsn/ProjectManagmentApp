using PMA_WorkTimeService.Models.DTOs;

namespace PMA_WorkTimeService.Services
{
    public interface IWorkTimeService
    {
        Task StartWork(int UserId);

        Task EndWork(int UserId);

        Task<UserWorkTimeDTO> GetUserWorkTimeInfo(int UserId);

        Task<IEnumerable<UserWorkTimeDTO>> GetAllUserWorkTimeInfo(int UserId);

    }
}
