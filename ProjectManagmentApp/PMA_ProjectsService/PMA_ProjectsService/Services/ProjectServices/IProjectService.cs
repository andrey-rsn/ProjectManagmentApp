using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAll(int limit = 10);
        Task<ProjectDTO> GetById(int id);
        Task<ProjectDTO> Update(ProjectDTO projectDTO);
        Task<ProjectDTO> Add(ProjectDTO projectDTO);
        Task<bool> DeleteById(int id);
    }
}
