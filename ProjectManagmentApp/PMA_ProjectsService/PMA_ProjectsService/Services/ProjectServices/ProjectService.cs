using AutoMapper;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
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

        public async Task<ProjectDTO> Update(ProjectDTO projectDTO)
        {
            var result = await _projectRepository.UpdateAsync(projectDTO);

            return result;
        }
    }
}
