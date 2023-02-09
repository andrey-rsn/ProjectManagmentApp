using Microsoft.AspNetCore.Mvc;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Services.UserTask;
using System.Net;

namespace PMA_TasksService.Controllers
{
    [Route("api/v1/userTask")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        // GET: api/v1/userTask?limit={limit}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTaskDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserTaskDTO>>> GetAll(int limit)
        {
            try
            {
                var userTasks = await _userTaskService.GetAll(limit);

                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/v1/userTask?userId={userId}&limit={limit}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTaskDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<UserTaskDTO>>> GetAllAssignedToUser(int userId, int limit)
        {
            try
            {
                var userTasks = await _userTaskService.GetAllByUserId(userId, limit);

                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/v1/userTask
        [HttpGet("{userTaskId}")]
        [ProducesResponseType(typeof(UserTaskDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserTaskDTO>> GetByUserTaskId(int userTaskId)
        {
            try
            {
                var userTask = await _userTaskService.GetById(userTaskId);

                return Ok(userTask);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/v1/userTask
        [HttpPost]
        [ProducesResponseType(typeof(UserTaskDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserTaskDTO>> Add([FromBody] UserTaskDTO userTask)
        {
            return Ok(await _userTaskService.Add(userTask));
        }

        // PUT api/v1/userTask
        [HttpPut]
        [ProducesResponseType(typeof(UserTaskDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserTaskDTO>> Update([FromBody] UserTaskDTO userTask)
        {
            return Ok(await _userTaskService.Update(userTask));
        }

        // DELETE api/v1/userTask/{userTaskId}
        [HttpDelete("{userTaskId}")]
        [ProducesResponseType(typeof(UserTaskDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserTaskDTO>> Delete(int userTaskId)
        {
            try
            {
                var deletedUserTask = await _userTaskService.Delete(userTaskId);

                return Ok(deletedUserTask);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
