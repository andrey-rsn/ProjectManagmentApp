using AutoMapper;
using PMA_ProjectsService.Models;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Repositories.Interfaces;

namespace PMA_ProjectsService.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeesAttachedToProjectsRepository _employeesAttachedToProjectsRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IEmployeesAttachedToProjectsRepository employeesAttachedToProjectsRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _employeesAttachedToProjectsRepository = employeesAttachedToProjectsRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Add(ProjectDTO projectDTO)
        {
            var result = await _projectRepository.AddAsync(projectDTO);  

            return result;
        }

        public async Task<bool> CreateProject(CreateProjectRequestModel projectInfo)
        {
            var IsAlreadyExists = (await _projectRepository.GetAsync(x=> string.Equals(x.Name ,projectInfo.Name))).Any();

            if(!IsAlreadyExists )
            {
                var project = _mapper.Map<ProjectDTO>(projectInfo);

                var addedProject = await _projectRepository.AddAsync(project);

                await _employeesAttachedToProjectsRepository.AddAsync(new EmployeesAttachedToProjectsDTO() { EmployeeId = projectInfo.AuthorId, ProjectId = addedProject.ProjectId });

                return true;
            }

            return false;
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

        public async Task<bool> IsUserAttachedToProject(int userId, int projectId)
        {
            var result = (await _employeesAttachedToProjectsRepository.GetAsync(x=>x.ProjectId==projectId && x.EmployeeId== userId, limit:1)).Any();

            return result;
        }

        public async Task<ProjectDTO> Update(ProjectDTO projectDTO)
        {
            var result = await _projectRepository.UpdateAsync(projectDTO);

            return result;
        }
    }
}
