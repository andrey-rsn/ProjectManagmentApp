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

            if((int)tasksResponse.StatusCode == 204) 
            {
                return NoContent();
            }

            var userTaskView = JsonSerializer.Deserialize<List<UserTaskViewModelIn>>(await tasksResponse.Content.ReadAsStringAsync());


            var UserTaskViews = _mapper.Map<IEnumerable<UserTaskViewModel>>(userTaskView);

            foreach(var userTask in UserTaskViews)
            {
                if(userTask.assignedUserId != 0)
                {
                    var assignedUserRequest = new HttpRequestMessage(
                         HttpMethod.Get,
                        _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.assignedUserId}");
                    var assignedUserResponse = await _identityClient.SendAsync(assignedUserRequest);

                    if (!assignedUserResponse.IsSuccessStatusCode)
                    {
                        return GetActionResultByStatusCode((int)assignedUserResponse.StatusCode);
                    }

                    if((int)assignedUserResponse.StatusCode == 204)
                    {
                        userTask.assignedUserId = 0;
                        userTask.assignedTo = "";
                    }
                    else
                    {
                        var assignedUser = JsonSerializer.Deserialize<UserInfoViewModel>(await assignedUserResponse.Content.ReadAsStringAsync());
                        userTask.assignedTo = assignedUser.FullName;
                    }
                    
                }
                
                if(userTask.changedByUserId != 0)
                {
                    var changedByRequest = new HttpRequestMessage(
                     HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.changedByUserId}");
                    var changedByResponse = await _identityClient.SendAsync(changedByRequest);

                    if (!changedByResponse.IsSuccessStatusCode)
                    {
                        return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
                    }

                    if((int)changedByResponse.StatusCode == 204)
                    {
                        userTask.changedByUserId = 0;
                        userTask.changedBy = "";
                    }
                    else
                    {
                        var changeByUser = JsonSerializer.Deserialize<UserInfoViewModel>(await changedByResponse.Content.ReadAsStringAsync());
                        userTask.changedBy = changeByUser.FullName;
                    }
                    
                }

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

            if (!tasksResponse.IsSuccessStatusCode || (int)tasksResponse.StatusCode == 204)
            {
                return GetActionResultByStatusCode((int)tasksResponse.StatusCode);
            }

            if((int)tasksResponse.StatusCode == 204)
            {
                return NoContent();
            }

            var userTaskViewModelIn = JsonSerializer.Deserialize<UserTaskViewModelIn>(await tasksResponse.Content.ReadAsStringAsync());

            var userTaskView = _mapper.Map<UserTaskViewModel>(userTaskViewModelIn);

            if(userTaskView.assignedUserId != 0)
            {
                var assignedUserRequest = new HttpRequestMessage(
                         HttpMethod.Get,
                        _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.assignedUserId}");
                var assignedUserResponse = await _identityClient.SendAsync(assignedUserRequest);

                if (!assignedUserResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)assignedUserResponse.StatusCode);
                }

                if((int)assignedUserResponse.StatusCode == 204)
                {
                    userTaskView.assignedTo = "";
                    userTaskView.changedByUserId = 0;
                    isConsistent= false;
                }
                else
                {
                    var assignedUser = JsonSerializer.Deserialize<UserInfoViewModel>(await assignedUserResponse.Content.ReadAsStringAsync());
                    userTaskView.assignedTo = assignedUser.FullName;
                }

            }
            
            if(userTaskView.changedByUserId!= 0)
            {
                var changedByRequest = new HttpRequestMessage(
                     HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.changedByUserId}");
                var changedByResponse = await _identityClient.SendAsync(changedByRequest);

                if (!changedByResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
                }

                if((int)changedByResponse.StatusCode == 204)
                {
                    userTaskView.changedBy = "";
                    userTaskView.changedByUserId = 0;
                    isConsistent = false;
                }
                else
                {
                    var changeByUser = JsonSerializer.Deserialize<UserInfoViewModel>(await changedByResponse.Content.ReadAsStringAsync());
                    userTaskView.changedBy = changeByUser.FullName;
                }
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

                if (!updateTaskResponse.IsSuccessStatusCode || (int)updateTaskResponse.StatusCode == 204)
                {
                    return GetActionResultByStatusCode((int)updateTaskResponse.StatusCode);
                }

                return Ok(userTaskView);
            }
            
        }

        // POST api/v1/userTask
        [HttpPost]
        public async Task<ActionResult> Add(UserTaskViewModelIn userTask)
        {
            var identityRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _identityClient.BaseAddress + $"api/v1/userInfo/{userTask.assignedUserId}");
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var identityResponse = await _identityClient.SendAsync(identityRequest);

            if (!identityResponse.IsSuccessStatusCode || (int)identityResponse.StatusCode == 204)
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

        // PUT api/v1/userTask
        [HttpPut]
        public async Task<ActionResult> Update (UserTaskViewModelIn userTaskView)
        {
            _tasksClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            if(userTaskView.assignedUserId != 0)
            {
                var assignedUserRequest = new HttpRequestMessage(
                         HttpMethod.Get,
                        _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.assignedUserId}");

                var assignedUserResponse = await _identityClient.SendAsync(assignedUserRequest);

                if (!assignedUserResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)assignedUserResponse.StatusCode);
                }

                if((int)assignedUserResponse.StatusCode == 204)
                {
                    userTaskView.changedByUserId = 0;
                }
                else
                {
                    var assignedUser = JsonSerializer.Deserialize<UserInfoViewModel>(await assignedUserResponse.Content.ReadAsStringAsync());
                }

            }

            if(userTaskView.changedByUserId != 0)
            {
                var changedByRequest = new HttpRequestMessage(
                 HttpMethod.Get,
                _identityClient.BaseAddress + $"api/v1/userInfo/{userTaskView.changedByUserId}");
                var changedByResponse = await _identityClient.SendAsync(changedByRequest);

                if (!changedByResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)changedByResponse.StatusCode);
                }

                if((int)changedByResponse.StatusCode == 204)
                {
                    userTaskView.changedByUserId = 0;
                }
                else
                {
                    var changeByUser = JsonSerializer.Deserialize<UserInfoViewModel>(await changedByResponse.Content.ReadAsStringAsync());
                }
            }

            var updateTaskRequest = new HttpRequestMessage(
                    HttpMethod.Put,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView");

            updateTaskRequest.Content = new StringContent(JsonSerializer.Serialize(userTaskView), Encoding.UTF8, "application/json");

            var updateTaskResponse = await _tasksClient.SendAsync(updateTaskRequest);

            if (!updateTaskResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)updateTaskResponse.StatusCode);
            }

            return Ok();
        }

        // DELETE api/v1/userTask/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById (int id)
        {
            _tasksClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var deleteTaskRequest = new HttpRequestMessage(
                    HttpMethod.Delete,
                    _tasksClient.BaseAddress + $"api/v1/userTaskView/{id}");


            var deleteTaskResponse = await _tasksClient.SendAsync(deleteTaskRequest);

            if (!deleteTaskResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)deleteTaskResponse.StatusCode);
            }

            return Ok();
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
