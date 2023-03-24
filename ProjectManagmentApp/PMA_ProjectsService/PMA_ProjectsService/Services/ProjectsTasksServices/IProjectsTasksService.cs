using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Services.ProjectsTasksServices
{
    public interface IProjectsTasksService
    {
        Task<IEnumerable<ProjectsTasksDTO>> GetByTaskId(int TaskId);
        Task<IEnumerable<ProjectsTasksDTO>> GetByTaskAndProjectId(int TaskId, int ProjectId);
        Task<IEnumerable<ProjectsTasksDTO>> GetByProjectId(int ProjectId);
        Task<ProjectsTasksDTO> Add(ProjectsTasksDTO projectsTasks);
        Task<bool> DeleteById(int Id);
        Task<bool> DeleteByTaskId(int taskId);

    }
}
