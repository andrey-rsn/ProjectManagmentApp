using AutoMapper;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeesAttachedToProjectsRepository _employeesAttachedToProjectsRepository;

        public ProjectService(IProjectRepository projectRepository, IEmployeesAttachedToProjectsRepository employeesAttachedToProjectsRepository)
        {
            _projectRepository = projectRepository;
            _employeesAttachedToProjectsRepository = employeesAttachedToProjectsRepository;
        }

        public async Task<ProjectDTO> Add(ProjectDTO projectDTO)
        {
            var result = await _projectRepository.AddAsync(projectDTO);  

            return result;
        }

        public async Task<bool> DeleteById(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if(project != null) 
            {
                var result = await _projectRepository.DeleteAsync(project);
                return result != null;
            }
            else
            {
                return false;
            }
                
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll(int limit = 10)
        {
            var projects = await _projectRepository.GetAllAsync(limit);

            return projects;
        }

        public async Task<ProjectDTO> GetById(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            return project;
        }

        public async Task<List<ProjectDTO>> GetByUserId(int userId)
        {
            var attachedEmployees = await _employeesAttachedToProjectsRepository.GetAsync(x=>x.EmployeeId== userId);

            var result = new List<ProjectDTO>();

            foreach(var projectId in attachedEmployees.Select(x=>x.ProjectId))
            {
                var project = await _projectRepository.GetByIdAsync(projectId);
                if(project != null)
                {
                    result.Add(project);
                }
            }

            return result;

        }

        public async Task<ProjectDTO> Update(ProjectDTO projectDTO)
        {
            var result = await _projectRepository.UpdateAsync(projectDTO);

            return result;
        }
    }
}
