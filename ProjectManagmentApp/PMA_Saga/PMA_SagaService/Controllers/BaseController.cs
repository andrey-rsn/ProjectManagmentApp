using Microsoft.AspNetCore.Mvc;

namespace PMA_SagaService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ActionResult GetActionResultByStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return Ok();
                case 401:
                    return Unauthorized();
                case 204:
                    return NoContent();
                default:
                    return BadRequest();
            }
        }
    }
}
