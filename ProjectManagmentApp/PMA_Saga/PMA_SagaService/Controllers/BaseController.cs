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
                case 403:
                    return Forbid();
                case 404:
                    return NotFound();
                case 409:
                    return Conflict();
                default:
                    return BadRequest();
            }
        }
    }
}
