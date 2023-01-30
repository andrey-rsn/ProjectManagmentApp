using PMA_WorkTimeService.Models.DTOs;

namespace PMA_WorkTimeService.Services
{
    public interface IWorkTimeService
    {
        Task<UserWorkTimeDTO> StartWork(int UserId);

        Task<UserWorkTimeDTO> EndWork(int UserId);

        Task<UserWorkTimeDTO> GetUserWorkTimeInfo(int UserId);

        Task<IEnumerable<UserWorkTimeDTO>> GetAllUserWorkTimeInfo(int UserId);

    }
}
