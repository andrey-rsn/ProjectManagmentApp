using Microsoft.AspNetCore.Mvc;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Services.ProjectServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_ProjectsService.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/v1/projects/all?limit={limit}
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll(int limit)
        {
            var result = await _projectService.GetAll(limit);

            if(result != null && result.Any()) 
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/v1/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetById(int id)
        {
            var result = await _projectService.GetById(id);
            if(result != null) 
            { 
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/v1/projects/byUser/{userId}
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetByUserId(int userId)
        {
            var result = await _projectService.GetByUserId(userId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/v1/projects/byUserAndProject?userId={userId}&projectId={projectId}
        [HttpGet("byUserAndProject")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetByUserAndProjectId(int userId,int projectId)
        {
            var result = await _projectService.GetById(projectId);
            if (result != null)
            {
                if(await _projectService.IsUserAttachedToProject(userId, projectId))
                {
                    return Ok(result);
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/v1/projects
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Add([FromBody] ProjectDTO project)
        {
            var result = await _projectService.Add(project);

            if(result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        // PUT api/v1/projects
        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> Update([FromBody] ProjectDTO project)
        {
            var result = await _projectService.Update(project);

            if(result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        // DELETE api/v1/projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _projectService.DeleteById(id);
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
