using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMA_WorkTimeService.Models;
using PMA_WorkTimeService.Models.DTOs;
using PMA_WorkTimeService.Services;
using System.Net;


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
        [ProducesResponseType(typeof(UserWorkTimeViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(UserWorkTimeViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(IEnumerable< UserWorkTimeViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(UserWorkTimeViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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

        // PUT api/v1/workTime/{UserId}
        [HttpPut("{UserId}")]
        [ProducesResponseType(typeof(UserWorkTimeViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserWorkTimeViewModel>> UpdateWorkTimeInfo(UserWorkTimeDTO userWorkTime)
        {
            try
            {
                var UserWorkTimeInfo = await _workTimeService.UpdateUserWorkTimeInfo(userWorkTime);

                return Ok(_mapper.Map<UserWorkTimeViewModel>(UserWorkTimeInfo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/v1/workTime/{UserId}
        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(UserWorkTimeViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserWorkTimeViewModel>> DeleteWorkTimeInfo(int WorkTimeInfoId)
        {
            try
            {
                await _workTimeService.DeleteUserWorkTimeInfo(WorkTimeInfoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
