using AutoMapper;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using PMA_WorkTimeService.Repositories;

namespace PMA_WorkTimeService.Services
{
    /// <summary>
    /// Service for operating with main business logic 
    /// </summary>
    public class WorkTimeService : IWorkTimeService
    {
        #region properties

        private readonly IWorkTimeRepository _workTimeRepository;
        private readonly IMapper _mapper;

        #endregion

        #region constructors

        public WorkTimeService(IWorkTimeRepository workTimeRepository, IMapper mapper = null)
        {
            _workTimeRepository = workTimeRepository;
            _mapper = mapper;
        }

        #endregion

        #region methods

        /// <summary>
        /// Add start work time info in database
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <returns>UserWorkTimeDTO model</returns>
        /// <exception cref="Exception">Throws when error occured</exception>
        public async Task<UserWorkTimeDTO> StartWork(int UserId)
        {
            if(UserId <= 0)
            {
                throw new Exception("UserId is not valid");
            }
            var WorkTime = new UserWorkTimeDTO 
            { 
                UserId = UserId,

                StartTime= DateTime.UtcNow
            };

            await _workTimeRepository.Add(WorkTime);

            return await _workTimeRepository.GetByUserId(UserId);
        }

        /// <summary>
        /// Update work time info by adding end time in database 
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <returns>UserWorkTimeDTO model</returns>
        /// <exception cref="Exception">Throws when error occured</exception>
        public async Task<UserWorkTimeDTO> EndWork(int UserId)
        {
            var WorkTime = await _workTimeRepository.GetByFilter(x=> x.UserId == UserId && x.StartTime != null);

            if(WorkTime != null) 
            {
                WorkTime.EndTime = DateTime.UtcNow;

                await _workTimeRepository.Update(WorkTime);
            }
            else
            {
                throw new Exception("User is not found");
            }

            return await _workTimeRepository.GetByUserId(UserId);
        }

        /// <summary>
        /// Get user work time info by user id
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <returns>UserWorkTimeDTO model</returns>
        /// <exception cref="Exception">Throws when error occured</exception>
        public async Task<UserWorkTimeDTO> GetUserWorkTimeInfo(int UserId)
        {
            var UserWorkTime = await _workTimeRepository.GetByUserId(UserId);

            if(UserWorkTime != null) 
            {
                return UserWorkTime;
            }
            else
            {
                throw new Exception("User is not found");
            }
        }

        /// <summary>
        /// Get all user work time info by user id with limit (1000 records)
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <returns>List of UserWorkTimeDTO model entities</returns>
        /// <exception cref="Exception">Throws when error occured</exception>
        public async Task<IEnumerable<UserWorkTimeDTO>> GetAllUserWorkTimeInfo(int UserId)
        {
            var UsersWorkTime = await _workTimeRepository.GetAll(1000);

            if(UsersWorkTime != null)
            {
                return UsersWorkTime;
            }
            throw new Exception("User is not found");
        }

        /// <summary>
        /// Update user work time info 
        /// </summary>
        /// <param name="UserWorkTimeInfo">UserWorkTimeInfoDTO model</param>
        /// <returns>UserWorkTimeInfoDTO mode</returns>
        public async Task<UserWorkTimeDTO> UpdateUserWorkTimeInfo(UserWorkTimeDTO UserWorkTimeInfo)
        {
            var UserWorkTime = _mapper.Map<UserWorkTime>(UserWorkTimeInfo);

            await _workTimeRepository.Update(UserWorkTimeInfo);

            return UserWorkTimeInfo;
        }

        /// <summary>
        /// Delete user work time info record from database by WorkTimeInfoId
        /// </summary>
        /// <param name="WorkTimeInfoId">WorkTimeInfoId</param>
        /// <returns></returns>
        public async Task DeleteUserWorkTimeInfo(int WorkTimeInfoId)
        {
            await _workTimeRepository.DeleteById(WorkTimeInfoId);
        }

        #endregion
    }
}
