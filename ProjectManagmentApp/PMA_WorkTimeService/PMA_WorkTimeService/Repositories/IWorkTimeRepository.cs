using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using System.Linq.Expressions;

namespace PMA_WorkTimeService.Repositories
{
    public interface IWorkTimeRepository : IBaseRepository<UserWorkTimeDTO>
    {
        Task<UserWorkTimeDTO> GetByUserId(int UserId);
        Task<UserWorkTimeDTO> GetByFilter(Expression<Func<UserWorkTime, bool>> filter);
    }
}
