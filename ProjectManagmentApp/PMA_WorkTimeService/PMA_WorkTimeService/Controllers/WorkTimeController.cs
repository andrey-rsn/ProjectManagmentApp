using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_WorkTimeService.Controllers
{
    [Route("api/v1/workTime")]
    [ApiController]
    public class WorkTimeController : ControllerBase
    {

        // POST api/v1/workTime
        [HttpPost]
        [Route("start")]
        public async Task<ActionResult> StartWork()
        {
            return Ok("start");
        }

    }
}
