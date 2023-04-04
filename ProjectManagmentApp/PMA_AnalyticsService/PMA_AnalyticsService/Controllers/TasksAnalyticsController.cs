using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_AnalyticsService.Services.TasksAnalyticsService;

namespace PMA_AnalyticsService.Controllers
{
    [Route("api/v1/analytics/tasks")]
    [ApiController]
    public class TasksAnalyticsController : ControllerBase
    {
        private readonly ITasksAnalyticsService _taskAnalyticsService;

        public TasksAnalyticsController(ITasksAnalyticsService taskAnalyticsService)
        {
            _taskAnalyticsService = taskAnalyticsService;
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<int[]>> GetTasksAnalyticsByProjectId(int projectId)
        {
            var result = await _taskAnalyticsService.GetTasksAnalyticsByProject(projectId);

            if(result.Length != 0)
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
