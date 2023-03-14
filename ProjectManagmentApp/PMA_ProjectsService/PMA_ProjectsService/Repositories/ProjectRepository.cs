using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Data;
using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Repositories
{
    public class ProjectRepository : BaseAsyncRepository<ProjectDTO, Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
