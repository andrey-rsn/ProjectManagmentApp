using AutoMapper;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using PMA_WorkTimeService.Repositories;

namespace PMA_WorkTimeService.Services
{
    public class WorkTimeService : IWorkTimeService
    {
        private readonly IWorkTimeRepository _workTimeRepository;

        public WorkTimeService(IWorkTimeRepository workTimeRepository)
        {
            _workTimeRepository = workTimeRepository;
        }

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

        public async Task<IEnumerable<UserWorkTimeDTO>> GetAllUserWorkTimeInfo(int UserId)
        {
            var UsersWorkTime = await _workTimeRepository.GetAll(1000);

            if(UsersWorkTime != null)
            {
                return UsersWorkTime;
            }
            throw new Exception("User is not found");
            
        }
    }
}
