using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Repositories;

namespace PMA_IdentityService.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<IEnumerable<PositionDTO>> GetAll()
        {
            return await _positionRepository.GetAll();
        }
    }
}
