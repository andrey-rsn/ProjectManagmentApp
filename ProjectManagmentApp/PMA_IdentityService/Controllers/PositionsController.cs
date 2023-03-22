using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMA_IdentityService.Models.DTOs;
using PMA_IdentityService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_IdentityService.Controllers
{
    [Route("api/v1/positions")]
    [Authorize]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/v1/positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDTO>>> GetAll()
        {
            var result = await _positionService.GetAll();

            if(result != null && result.Any()) 
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
