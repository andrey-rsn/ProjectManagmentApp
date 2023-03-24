using Microsoft.AspNetCore.Mvc;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Services.ProjectsTasksServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_ProjectsService.Controllers
{
    [Route("api/projectsTasks")]
    [ApiController]
    public class ProjectsTasksController : ControllerBase
    {
        private readonly IProjectsTasksService _projectsTasksService;

        public ProjectsTasksController(IProjectsTasksService projectsTasksService)
        {
            _projectsTasksService = projectsTasksService;
        }

        // GET: api/projectsTasks/byProject/{projectId}
        [HttpGet("byProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectsTasksDTO>>> GetByProjectId(int projectId)
        {
            var projects = await _projectsTasksService.GetByProjectId(projectId);

            if(projects != null && projects.Any()) 
            {
                return Ok(projects);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/projectsTasks/byTask/{taskId}
        [HttpGet("byTask/{taskId}")]
        public async Task<ActionResult<IEnumerable<ProjectsTasksDTO>>> GetByTaskId(int taskId)
        {
            var projects = await _projectsTasksService.GetByTaskId(taskId);

            if (projects != null && projects.Any())
            {
                return Ok(projects);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/projectsTasks/byTaskAndProject?taskId={taskId}&projectId={projectId}
        [HttpGet("byTaskAndProject")]
        public async Task<ActionResult<IEnumerable<ProjectsTasksDTO>>> GetByTaskAndProjectId(int taskId, int projectId)
        {
            var projects = await _projectsTasksService.GetByTaskAndProjectId(taskId, projectId);

            if (projects != null && projects.Any())
            {
                return Ok(projects);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/projectsTasks
        [HttpPost]
        public async Task<ActionResult<ProjectsTasksDTO>> Add([FromBody] ProjectsTasksDTO entity)
        {
            var result = await _projectsTasksService.Add(entity);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE api/projectsTasks/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await _projectsTasksService.DeleteById(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/projectsTasks/byTask/{taskId}
        [HttpDelete("byTask/{taskId}")]
        public async Task<ActionResult> DeleteByProjectId(int taskId)
        {
            var result = await _projectsTasksService.DeleteByTaskId(taskId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
