using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Services.ProjectsTasksServices
{
    public class ProjectsTasksService : IProjectsTasksService
    {
        private readonly IProjectsTasksRepository _projectsTasksRepository;

        public ProjectsTasksService(IProjectsTasksRepository projectsTasksRepository)
        {
            _projectsTasksRepository = projectsTasksRepository;
        }

        public async Task<ProjectsTasksDTO> Add(ProjectsTasksDTO projectsTasks)
        {
           var result = await _projectsTasksRepository.AddAsync(projectsTasks);
            return result;
        }

        public async Task<bool> DeleteById(int Id)
        {
            var project = await _projectsTasksRepository.GetByIdAsync(Id);

            if(project != null) 
            {
                await _projectsTasksRepository.DeleteAsync(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteByTaskId(int taskId)
        {
            var projectsToDelete = await _projectsTasksRepository.GetAsync(x=>x.TaskId == taskId, null, null, true);

            if (projectsToDelete != null && projectsToDelete.Any())
            {
                foreach(var project in projectsToDelete)
                {
                    await _projectsTasksRepository.DeleteAsync(project);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProjectsTasksDTO>> GetByProjectId(int ProjectId)
        {
            var Projects = await _projectsTasksRepository.GetAsync(x => x.ProjectId == ProjectId);

            return Projects;
        }

        public async Task<IEnumerable<ProjectsTasksDTO>> GetByTaskAndProjectId(int TaskId, int ProjectId)
        {
            var Projects = await _projectsTasksRepository.GetAsync(x => x.ProjectId == ProjectId && x.TaskId == TaskId);

            return Projects;
        }

        public async Task<IEnumerable<ProjectsTasksDTO>> GetByTaskId(int TaskId)
        {
            var Projects = await _projectsTasksRepository.GetAsync(x => x.TaskId == TaskId);

            return Projects;
        }
    }
}
