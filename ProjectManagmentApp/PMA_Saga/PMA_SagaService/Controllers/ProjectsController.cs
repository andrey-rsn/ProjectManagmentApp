using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMA_SagaService.Models;
using System.Collections.Generic;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMA_SagaService.Controllers
{
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly HttpClient _projectsClient;
        private readonly HttpClient _identityClient;
        private readonly IMapper _mapper;

        public ProjectsController(IHttpClientFactory httpClientFactory ,IMapper mapper)
        {
            _projectsClient = httpClientFactory.CreateClient("projectsServiceClient");
            _identityClient = httpClientFactory.CreateClient("identityServiceClient");
            _mapper = mapper;
        }


        // GET: api/v1/projects/byUser/{userId}
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetByUserId(int userId)
        {
            _projectsClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var userProjectsRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _projectsClient.BaseAddress + $"api/v1/projects/byUser/{userId}");

            var userProjectsResponse = await _projectsClient.SendAsync(userProjectsRequest);

            if (!userProjectsResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)userProjectsResponse.StatusCode);
            }

            if ((int)userProjectsResponse.StatusCode == 204)
            {
                return NoContent();
            }

            var userProjects = JsonSerializer.Deserialize<List<ProjectViewModel>>(await userProjectsResponse.Content.ReadAsStringAsync());

            return Ok(userProjects);

        }

        // GET: api/v1/projects/byUserAndProject?userId={userId}&projectId={projectId}
        [HttpGet("byUserAndProject")]
        public async Task<ActionResult<ProjectViewModel>> GetByUserAndProjectId(int userId, int projectId)
        {
            _projectsClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var projectRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _projectsClient.BaseAddress + $"api/v1/projects/byUserAndProject?userId={userId}&projectId={projectId}");

            var projectResponse = await _projectsClient.SendAsync(projectRequest);

            if (!projectResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)projectResponse.StatusCode);
            }

            if ((int)projectResponse.StatusCode == 204)
            {
                return NoContent();
            }

            var project = JsonSerializer.Deserialize<ProjectViewModel>(await projectResponse.Content.ReadAsStringAsync());

            return Ok(project);

        }

    }
}
