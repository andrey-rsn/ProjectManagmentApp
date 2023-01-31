using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_WorkTimeService.Data;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using System.Linq.Expressions;

namespace PMA_WorkTimeService.Repositories
{
    /// <summary>
    /// Repository for operating with WorkTime database entities
    /// </summary>
    public class WorkTimeRepository : IWorkTimeRepository
    {
        #region properties

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        #endregion

        #region constructors

        public WorkTimeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        #region methods

        /// <summary>
        /// Add record in database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public async Task Add(UserWorkTimeDTO entity)
        {
            var WorkTime = _mapper.Map<UserWorkTime>(entity);

            await _dbContext.UsersWorkTime.AddAsync(WorkTime);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Add range of entities in database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public async Task AddRange(IEnumerable<UserWorkTimeDTO> entity)
        {
            var WorkTimeList = _mapper.Map<IEnumerable<UserWorkTime>>(entity);

            await _dbContext.UsersWorkTime.AddRangeAsync(WorkTimeList);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity from database by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var WorkTime = await _dbContext.UsersWorkTime.FindAsync(id);

            if(WorkTime != null)
            {
                _dbContext.UsersWorkTime.Remove(WorkTime);

                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all entites from database with limit
        /// </summary>
        /// <param name="limit">Limit</param>
        /// <returns>List of UserWorkTimeDTO models</returns>
        public async Task<IEnumerable<UserWorkTimeDTO>> GetAll(int limit = 1000)
        {
            var WorkTimeList = await _dbContext.UsersWorkTime.AsNoTracking().OrderBy(x=>x.UserWorkTimeId).Take(limit).ToListAsync();

            return _mapper.Map<IEnumerable<UserWorkTimeDTO>>(WorkTimeList);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>UserWorkTimeDTO model</returns>
        public async Task<UserWorkTimeDTO> GetById(int id)
        {
            var WorkTime = await _dbContext.UsersWorkTime.FindAsync(id);

            return _mapper.Map<UserWorkTimeDTO>(WorkTime);
        }

        /// <summary>
        /// Get last user work time record by user id
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <returns>UserWorkTimeDTO model</returns>
        public async Task<UserWorkTimeDTO> GetByUserId(int UserId)
        {
            var WorkTime = await _dbContext.UsersWorkTime.AsNoTracking().OrderBy(x=>x.UserWorkTimeId).LastOrDefaultAsync(x=>x.UserId == UserId);

            return _mapper.Map<UserWorkTimeDTO>(WorkTime);
        }

        /// <summary>
        /// Get entity from database by filter
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>UserWorkTimeDTO model</returns>
        public async Task<UserWorkTimeDTO> GetByFilter(Expression<Func<UserWorkTime,bool>> filter)
        {
            var WorkTime = await _dbContext.UsersWorkTime.AsNoTracking().OrderBy(x => x.UserWorkTimeId).LastOrDefaultAsync(filter);

            return _mapper.Map<UserWorkTimeDTO>(WorkTime);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity model</param>
        /// <returns></returns>
        public async Task Update(UserWorkTimeDTO entity)
        {
            var WorkTime = _mapper.Map<UserWorkTime>(entity); 

            _dbContext.Update(WorkTime);

            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
