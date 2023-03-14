using Microsoft.AspNetCore.Mvc;
using PMA_ProjectsService.Models.DTOs;
using PMA_ProjectsService.Services.EmployeesAttachedToProjectsServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_ProjectsService.Controllers
{
    [Route("api/v1/employeesAttachedToProjects")]
    [ApiController]
    public class EmployeesAttachedToProjectsController : ControllerBase
    {
        private readonly IEmployeesAttachedToProjectsService _employeesAttachedToProjectsService;

        public EmployeesAttachedToProjectsController(IEmployeesAttachedToProjectsService employeesAttachedToProjectsService)
        {
            _employeesAttachedToProjectsService = employeesAttachedToProjectsService;
        }

        // GET: api/v1/employeesAttachedToProjects/byUser/{userId}
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<EmployeesAttachedToProjectsDTO>>> GetByUserId(int userId)
        {
            var result = await _employeesAttachedToProjectsService.GetByUserId(userId);

            if(result != null) 
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // GET: api/v1/employeesAttachedToProjects/byProject/{projectId}
        [HttpGet("{projectId}")]
        public async Task<ActionResult<IEnumerable<EmployeesAttachedToProjectsDTO>>> GetByProjectId(int projectId)
        {
            var result = await _employeesAttachedToProjectsService.GetByProjectId(projectId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }


        // POST api/v1/employeesAttachedToProjects
        [HttpPost]
        public async Task<ActionResult<EmployeesAttachedToProjectsDTO>> Add([FromBody] EmployeesAttachedToProjectsDTO entity)
        {
            var result = await _employeesAttachedToProjectsService.Add(entity);

            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        // DELETE api/v1/employeesAttachedToProjects/byUserAndProject?userId={userId}&projectId={projectId}
        [HttpDelete]
        public async Task<ActionResult> DeleteByUserAndProjectId(int userId, int projectId)
        {
            var result = await _employeesAttachedToProjectsService.DeleteByUserAndProjectId(userId, projectId);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
