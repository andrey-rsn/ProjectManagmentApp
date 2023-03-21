using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Services.EmployeesAttachedToProjectsServices
{
    public interface IEmployeesAttachedToProjectsService
    {
        Task<IEnumerable<EmployeesAttachedToProjectsDTO>> GetByUserId(int UserId);
        Task<IEnumerable<EmployeesAttachedToProjectsDTO>> GetByProjectId(int ProjectId);
        Task<EmployeesAttachedToProjectsDTO> Add(EmployeesAttachedToProjectsDTO EmployeesAttachedToProjects);
        Task<bool> AddRange(IEnumerable<EmployeesAttachedToProjectsDTO> EmployeesAttachedToProjects);
        Task<bool> DeleteByUserAndProjectId(int UserId, int ProjectId);
    }
}
