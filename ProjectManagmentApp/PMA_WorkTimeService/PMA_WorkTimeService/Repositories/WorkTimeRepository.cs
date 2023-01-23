using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_WorkTimeService.Data;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;

namespace PMA_WorkTimeService.Repositories
{
    public class WorkTimeRepository : IWorkTimeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkTimeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Add(UserWorkTimeDTO entity)
        {
            var WorkTime = _mapper.Map<UserWorkTime>(entity);

            await _dbContext.UsersWorkTime.AddAsync(WorkTime);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<UserWorkTimeDTO> entity)
        {
            var WorkTimeList = _mapper.Map<IEnumerable<UserWorkTime>>(entity);

            await _dbContext.UsersWorkTime.AddRangeAsync(WorkTimeList);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var WorkTime = await _dbContext.UsersWorkTime.FindAsync(id);

            if(WorkTime != null)
            {
                _dbContext.UsersWorkTime.Remove(WorkTime);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserWorkTimeDTO>> GetAll()
        {
            var WorkTimeList = await _dbContext.UsersWorkTime.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<UserWorkTimeDTO>>(WorkTimeList);
        }

        public async Task<UserWorkTimeDTO> GetById(int id)
        {
            var WorkTime = await _dbContext.UsersWorkTime.FindAsync(id);

            return _mapper.Map<UserWorkTimeDTO>(WorkTime);
        }

        public async Task<UserWorkTimeDTO> GetByUserId(int UserId)
        {
            var WorkTime = await _dbContext.UsersWorkTime.LastOrDefaultAsync(x=>x.UserId == UserId);

            return _mapper.Map<UserWorkTimeDTO>(WorkTime);
        }

        public async Task Update(UserWorkTimeDTO entity)
        {
            var WorkTime = await _dbContext.UsersWorkTime.FirstOrDefaultAsync(x=>x.UserWorkTimeId == entity.UserWorkTimeId);

            _dbContext.Update(WorkTime);

            await _dbContext.SaveChangesAsync();
        }
    }
}
