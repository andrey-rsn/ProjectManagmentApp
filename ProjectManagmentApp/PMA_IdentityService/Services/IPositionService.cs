using PMA_IdentityService.Models.DTOs;

namespace PMA_IdentityService.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDTO>> GetAll();
    }
}
