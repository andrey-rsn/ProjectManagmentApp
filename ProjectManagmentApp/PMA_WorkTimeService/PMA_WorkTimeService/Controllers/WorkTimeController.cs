using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_WorkTimeService.Controllers
{
    [Route("api/v1/workTime")]
    [ApiController]
    public class WorkTimeController : ControllerBase
    {
        private readonly IWorkTimeService _workTimeService;
        private readonly IMapper _mapper;

        public WorkTimeController(IWorkTimeService workTimeService, IMapper mapper)
        {
            _workTimeService = workTimeService;
            _mapper = mapper;
        }

        // POST api/v1/workTime/start
        [HttpPost]
        [Route("start")]
        public async Task<ActionResult> StartWork(int UserId)
        {
            try
            {
                await _workTimeService.StartWork(UserId);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST api/v1/workTime/end
        [HttpPost]
        [Route("end")]
        public async Task<ActionResult> EndWork(int UserId)
        {
            try
            {
                await _workTimeService.EndWork(UserId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/v1/workTime
        [HttpGet("{UserId}")]
        public async Task<ActionResult<UserWorkTimeViewModel>> GetAllWorkTimeInfo(int UserId)
        {
            try
            {
                var UsersWorkTimeInfos = await _workTimeService.GetAllUserWorkTimeInfo(UserId);

                return Ok(_mapper.Map<IEnumerable<UserWorkTimeViewModel>>(UsersWorkTimeInfos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
