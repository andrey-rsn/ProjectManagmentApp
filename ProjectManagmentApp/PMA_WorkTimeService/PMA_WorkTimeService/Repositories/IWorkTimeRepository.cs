using PMA_WorkTimeService.Models.DTOs;

namespace PMA_WorkTimeService.Repositories
{
    public interface IWorkTimeRepository : IBaseRepository<UserWorkTimeDTO>
    {
        Task<UserWorkTimeDTO> GetByUserId(int UserId);
    }
}
