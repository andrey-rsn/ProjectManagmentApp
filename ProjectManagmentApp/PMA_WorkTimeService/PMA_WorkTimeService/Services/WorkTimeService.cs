using AutoMapper;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using PMA_WorkTimeService.Repositories;

namespace PMA_WorkTimeService.Services
{
    public class WorkTimeService
    {
        private readonly IWorkTimeRepository _workTimeRepository;

        public WorkTimeService(IWorkTimeRepository workTimeRepository)
        {
            _workTimeRepository = workTimeRepository;
        }

        public async Task StartWork(int UserId)
        {
            var WorkTime = new UserWorkTimeDTO 
            { 
                UserId = UserId,

                StartTime= DateTime.UtcNow
            };

            await _workTimeRepository.Add(WorkTime);
        }

        public async Task EndWork(int UserId)
        {
            var WorkTime = await _workTimeRepository.GetByUserId(UserId);

            if(WorkTime != null) 
            {
                WorkTime.EndTime = DateTime.UtcNow;

                await _workTimeRepository.Add(WorkTime);
            }
            else
            {
                throw new Exception("User is not found");
            }

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
    }
}
