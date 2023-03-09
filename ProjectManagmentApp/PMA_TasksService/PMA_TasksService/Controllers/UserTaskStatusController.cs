using Microsoft.AspNetCore.Mvc;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Services.UserTaskStatusServices;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_TasksService.Controllers
{
    [Route("api/v1/userTaskStatus")]
    [ApiController]
    public class UserTaskStatusController : ControllerBase
    {
        private readonly IUserTaskStatusService _userTaskStatusService;

        public UserTaskStatusController(IUserTaskStatusService userTaskStatusService)
        {
            _userTaskStatusService = userTaskStatusService;
        }

        // GET: api/v1/userTaskStatus?limit={limit}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTaskStatusDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<UserTaskStatusDTO>>> GetAll(int limit)
        {
            try
            {
                var userTaskStatus = await _userTaskStatusService.GetAll(limit);

                return Ok(userTaskStatus);
            }
            catch 
            {
                return NoContent();
            }
        }

        // GET: api/v1/userTaskStatus/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserTaskStatusDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<UserTaskStatusDTO>> GetById(int id)
        {
            try
            {
                var userTaskStatus = await _userTaskStatusService.GetById(id);

                return Ok(userTaskStatus);
            }
            catch
            {
                return NoContent();
            }
        }

    }
}
