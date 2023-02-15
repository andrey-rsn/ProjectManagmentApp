using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMA_SagaService.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_SagaService.Controllers
{
    [Route("api/v1/userTask")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly HttpClient _tasksClient;
        private readonly HttpClient _identityClient;
        private readonly IMapper _mapper;
        public UserTaskController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _tasksClient = httpClientFactory.CreateClient("tasksServiceClient");
            _identityClient = httpClientFactory.CreateClient("identityServiceClient");
            _mapper = mapper;
        }

        // GET: api/v1/userTask/all?limit={limit}
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserTaskViewModel>>> GetAll(int limit = 10)
        {
            _tasksClient.DefaultRequestHeaders.Add("Authorization" , Convert.ToString(HttpContext.Request.Headers.Authorization));
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var tasksRequest = new HttpRequestMessage(
                    HttpMethod.Get,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView?limit={limit}");
            

            var tasksResponse = await _tasksClient.SendAsync(tasksRequest);

            if (!tasksResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)tasksResponse.StatusCode);
            }

            var userTaskView = JsonSerializer.Deserialize<List<UserTaskViewModelIn>>(await tasksResponse.Content.ReadAsStringAsync());

            if (userTaskView == null || !userTaskView.Any())
            {
                return NoContent();
            }

            var UserTaskViews = _mapper.Map<IEnumerable<UserTaskViewModel>>(userTaskView);

            foreach(var userTask in UserTaskViews)
            {
                var assignedUserRequest = new HttpRequestMessage(
                     HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.assignedUserId}");
                var assignedUserResponse = await _identityClient.SendAsync(assignedUserRequest);

                if (!assignedUserResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)assignedUserResponse.StatusCode);
                }

                var assignedUser = JsonSerializer.Deserialize<UserInfoViewModel>(await assignedUserResponse.Content.ReadAsStringAsync());

                if (assignedUser == null)
                {
                    return NoContent();
                }

                userTask.assignedTo = assignedUser.FullName;

                var changedByRequest = new HttpRequestMessage(
                     HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.changedByUserId}");
                var changedByResponse = await _identityClient.SendAsync(changedByRequest);

                if (!changedByResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
                }

                var changeByUser = JsonSerializer.Deserialize<UserInfoViewModel>(await changedByResponse.Content.ReadAsStringAsync());

                if (changeByUser == null)
                {
                    return NoContent();
                }

                userTask.changedBy = changeByUser.FullName;
            }

            return Ok(UserTaskViews);
        }

        // GET api/v1/userTask/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskViewModel>> GetById(int id)
        {
            bool isConsistent = true;
            _tasksClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var tasksRequest = new HttpRequestMessage(
                    HttpMethod.Get,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView/{id}");


            var tasksResponse = await _tasksClient.SendAsync(tasksRequest);

            if (!tasksResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)tasksResponse.StatusCode);
            }

            var userTaskViewModelIn = JsonSerializer.Deserialize<UserTaskViewModelIn>(await tasksResponse.Content.ReadAsStringAsync());

            if (userTaskViewModelIn == null)
            {
                return NoContent();
            }

            var userTaskView = _mapper.Map<UserTaskViewModel>(userTaskViewModelIn);

            var assignedUserRequest = new HttpRequestMessage(
                     HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.assignedUserId}");
            var assignedUserResponse = await _identityClient.SendAsync(assignedUserRequest);

            if (!assignedUserResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)assignedUserResponse.StatusCode);
            }

            var assignedUser = JsonSerializer.Deserialize<UserInfoViewModel>(await assignedUserResponse.Content.ReadAsStringAsync());

            if (assignedUser == null)
            {
                userTaskView.assignedTo = "";
                userTaskView.changedByUserId = 0;

                isConsistent= false;

                return NoContent();
            }
            else
            {
                userTaskView.assignedTo = assignedUser.FullName;
            }


            var changedByRequest = new HttpRequestMessage(
                 HttpMethod.Get,
                _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.changedByUserId}");
            var changedByResponse = await _identityClient.SendAsync(changedByRequest);

            if (!changedByResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
            }

            var changeByUser = JsonSerializer.Deserialize<UserInfoViewModel>(await changedByResponse.Content.ReadAsStringAsync());

            if (changeByUser == null)
            {
                userTaskView.changedBy = "";
                userTaskView.changedByUserId = 0;
                isConsistent= false;
                return NoContent();
            }
            else
            {
                userTaskView.changedBy = changeByUser.FullName;
            }

            if(isConsistent)
            {
                return Ok(userTaskView);
            }
            else
            {
                var updatedTask = _mapper.Map<UserTaskViewModelIn>(userTaskView);

                var updateTaskRequest = new HttpRequestMessage(
                    HttpMethod.Put,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView");

                updateTaskRequest.Content = new StringContent(JsonSerializer.Serialize(updatedTask), Encoding.UTF8, "application/json");

                var updateTaskResponse = await _tasksClient.SendAsync(tasksRequest);

                if (!updateTaskResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
                }

                return Ok(userTaskView);
            }
            
        }

        // POST api/v1/userTask
        [HttpPost]
        public async Task<ActionResult<UserTaskViewModelIn>> Add(UserTaskViewModelIn userTask)
        {
            var identityRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.assignedUserId}");
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var identityResponse = await _identityClient.SendAsync(identityRequest);

            if (!identityResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)identityResponse.StatusCode);
            }

            var tasksRequest = new HttpRequestMessage(
                    HttpMethod.Post,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView");

            tasksRequest.Content = new StringContent(JsonSerializer.Serialize(userTask), Encoding.UTF8, "application/json");

            _tasksClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var tasksResponse = await _tasksClient.SendAsync(tasksRequest);

            if(!tasksResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)tasksResponse.StatusCode);
            }

            return Ok();

        }

        // PUT api/<UserTaskController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserTaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private ActionResult GetActionResultByStatusCode(int statusCode)
        {
            switch(statusCode)
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
