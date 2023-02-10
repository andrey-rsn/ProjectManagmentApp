using Microsoft.AspNetCore.Mvc;
using PMA_TasksService.Models.DTOs;
using PMA_TasksService.Services.TaskCommentsServices;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_TasksService.Controllers
{
    [Route("api/v1/taskComments")]
    [ApiController]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ITaskCommentsService _taskCommentsService;

        public TaskCommentsController(ITaskCommentsService taskCommentsService)
        {
            _taskCommentsService = taskCommentsService;
        }

        // GET: api/v1/taskComments?limit={limit}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskCommentsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<TaskCommentsDTO>>> GetAll(int limit)
        {
            try
            {
                var taskComments = await _taskCommentsService.GetAll(limit);

                return Ok(taskComments);
            }
            catch
            {
                return NoContent();
            }
            
        }

        // GET api/v1/taskComments/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<TaskCommentsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<TaskCommentsDTO>> GetById(int id)
        {
            try
            {
                var taskComment = await _taskCommentsService.GetById(id);

                return Ok(taskComment);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        // GET api/v1/taskComments/byTask/{taskId}
        [HttpGet("byTask/{taskId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskCommentsDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<TaskCommentsDTO>>> GetByTaskId(int taskId)
        {
            try
            {
                var taskComment = await _taskCommentsService.GetByTaskId(taskId);

                return Ok(taskComment);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        // POST api/v1/taskComments
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<TaskCommentsDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaskCommentsDTO>> Add([FromBody] TaskCommentsDTO taskComment)
        {
            return Ok(await _taskCommentsService.Add(taskComment));
        }

        // PUT api/v1/taskComments
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<TaskCommentsDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TaskCommentsDTO>> Update([FromBody] TaskCommentsDTO taskComment)
        {
            return Ok(await _taskCommentsService.Update(taskComment));
        }

        // DELETE api/v1/taskComments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskCommentsDTO>> Delete(int id)
        {
            try
            {
                var deletedTaskComment = await _taskCommentsService.Delete(id);

                return Ok(deletedTaskComment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
