using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Repositories
{
    public class ProjectsTasksRepository : BaseAsyncRepository<ProjectsTasksDTO, ProjectsTasksModel>, IProjectsTasksRepository
    {
        public ProjectsTasksRepository(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
