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

        // POST api/v1/workTime/start?UserId = {UserId}
        [HttpPost]
        [Route("start")]
        public async Task<ActionResult<UserWorkTimeViewModel>> StartWork(int UserId)
        {
            try
            {
                var UserWorkTime = await _workTimeService.StartWork(UserId);

                var Result = _mapper.Map<UserWorkTimeViewModel>(UserWorkTime);

                return Ok(Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST api/v1/workTime/end?UserId = {UserId}
        [HttpPost]
        [Route("end")]
        public async Task<ActionResult<UserWorkTimeViewModel>> EndWork(int UserId)
        {
            try
            {
                var UserWorkTime = await _workTimeService.EndWork(UserId);

                var Result = _mapper.Map<UserWorkTimeViewModel>(UserWorkTime);

                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/v1/workTime/all/{UserId}
        [HttpGet("all/{UserId}")]
        public async Task<ActionResult<IEnumerable<UserWorkTimeViewModel>>> GetAllWorkTimeInfo(int UserId)
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

        // GET api/v1/workTime/last/{UserId}
        [HttpGet("last/{UserId}")]
        public async Task<ActionResult<UserWorkTimeViewModel>> GetLastWorkTimeInfo(int UserId)
        {
            try
            {
                var LastUserWorkTimeInfo = await _workTimeService.GetUserWorkTimeInfo(UserId);

                return Ok(_mapper.Map<UserWorkTimeViewModel>(LastUserWorkTimeInfo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
