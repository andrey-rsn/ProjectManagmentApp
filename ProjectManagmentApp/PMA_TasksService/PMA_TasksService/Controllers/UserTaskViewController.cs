using Microsoft.AspNetCore.Mvc;
using PMA_TasksService.Models;
using PMA_TasksService.Services.UserTaskServices;


namespace PMA_TasksService.Controllers
{
    [Route("api/v1/userTaskView")]
    [ApiController]
    public class UserTaskViewController : ControllerBase
    {
        private readonly IUserTaskViewService _userTaskViewService;

        public UserTaskViewController(IUserTaskViewService userTaskViewService)
        {
            _userTaskViewService = userTaskViewService;
        }

        // GET: api/v1/userTaskView?limit={limit}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTaskViewModel>>> GetAll(int limit = 100)
        {
            try
            {
                var userTasksViews = await _userTaskViewService.GetAll(limit);

                return Ok(userTasksViews);
            }
            catch
            {
                return NoContent();
            }
            
        }

        // GET api/v1/userTaskView/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskViewModel>> GetById(int id)
        {
            try
            {
                var userTaskView = await _userTaskViewService.GetById(id);

                return Ok(userTaskView);
            }
            catch 
            {
                return NoContent();
            }
        }

        // POST api/v1/userTaskView
        [HttpPost]
        public async Task<ActionResult<UserTaskViewModel>> Add([FromBody] UserTaskViewModel userTask)
        {
            try
            {
                var result = await _userTaskViewService.Add(userTask);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/v1/userTaskView
        [HttpPut]
        public async Task<ActionResult<UserTaskViewModel>> Update([FromBody] UserTaskViewModel userTask)
        {
            var result = await _userTaskViewService.Update(userTask);

            return Ok(result);
        }

        // DELETE api/v1/userTaskView/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTaskViewModel>> Delete(int id)
        {
            try
            {
                var result = await _userTaskViewService.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
