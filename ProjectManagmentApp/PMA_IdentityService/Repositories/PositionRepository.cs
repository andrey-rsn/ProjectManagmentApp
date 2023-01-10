using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_IdentityService.Data;
using PMA_IdentityService.Models;
using PMA_IdentityService.Models.DTOs;

namespace PMA_IdentityService.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PositionRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Add(PositionDTO entity)
        {
            var PositionEntity = _mapper.Map<Position>(entity);

            _dbContext.Positions.Add(PositionEntity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<PositionDTO> entity)
        {
            var PositionEntities = _mapper.Map<IEnumerable<Position>>(entity);

            _dbContext.Positions.AddRange(PositionEntities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var Position = await _dbContext.Positions.FindAsync(id);

            if (Position != null)
            {
                _dbContext.Positions.Remove(Position);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PositionDTO>> GetAll()
        {
            var Positions = await _dbContext.Positions.ToListAsync();

            return _mapper.Map<IEnumerable<PositionDTO>>(Positions);
        }

        public async Task<PositionDTO> GetById(int id)
        {
            var Position = await _dbContext.Positions.FindAsync(id);

            return _mapper.Map<PositionDTO>(Position);
        }

        public async Task Update(PositionDTO entity)
        {
            var Position = _mapper.Map<Position>(entity);

            _dbContext.Update(Position);

            await _dbContext.SaveChangesAsync();
        }
    }
}
