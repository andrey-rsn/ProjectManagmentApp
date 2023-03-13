using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Repositories.Interfaces
{
    public interface IProjectsTasksRepository : IBaseAsyncRepository<ProjectsTasksDTO,ProjectsTasksModel>
    {
    }
}
