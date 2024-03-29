﻿using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;

namespace PMA_ProjectsService.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAll(int limit = 10);
        Task<ProjectDTO> GetById(int id);
        Task<ProjectDTO> Update(ProjectDTO projectDTO);
        Task<ProjectDTO> Add(ProjectDTO projectDTO);
        Task<bool> CreateProject(CreateProjectRequestModel projectInfo);
        Task<bool> DeleteById(int id);
        Task<List<ProjectDTO>> GetByUserId(int userId);
        Task<bool> IsUserAttachedToProject(int userId,int projectId);
    }
}
