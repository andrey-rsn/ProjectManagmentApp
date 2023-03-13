using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Services.EmployeesAttachedToProjectsServices
{
    public class EmployeesAttachedToProjectsService : IEmployeesAttachedToProjectsService
    {
        private readonly IEmployeesAttachedToProjectsRepository _employeesAttachedToProjectsRepository;

        public EmployeesAttachedToProjectsService(IEmployeesAttachedToProjectsRepository employeesAttachedToProjectsRepository)
        {
            _employeesAttachedToProjectsRepository = employeesAttachedToProjectsRepository;
        }

        public async Task<EmployeesAttachedToProjectsDTO> Add(EmployeesAttachedToProjectsDTO EmployeesAttachedToProjects)
        {
            var result = await _employeesAttachedToProjectsRepository.AddAsync(EmployeesAttachedToProjects);

            return result;
        }

        public async Task<bool> DeleteByUserAndProjectId(int UserId, int ProjectId)
        {
            var entities = await _employeesAttachedToProjectsRepository.GetAsync(x=>x.ProjectId== ProjectId && x.EmployeeId == UserId);

            if(entities != null && entities.Any())
            {
                foreach(var entity in entities)
                {
                    await _employeesAttachedToProjectsRepository.DeleteAsync(entity);
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<EmployeesAttachedToProjectsDTO>> GetByProjectId(int ProjectId)
        {
            var result = await _employeesAttachedToProjectsRepository.GetAsync(x=> x.ProjectId== ProjectId);

            return result;
        }

        public async Task<IEnumerable<EmployeesAttachedToProjectsDTO>> GetByUserId(int UserId)
        {
            var result = await _employeesAttachedToProjectsRepository.GetAsync(x => x.EmployeeId == UserId);

            return result;
        }
    }
}
