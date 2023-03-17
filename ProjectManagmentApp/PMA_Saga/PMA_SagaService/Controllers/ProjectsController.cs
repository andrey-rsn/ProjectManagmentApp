﻿using AutoMapper;
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

        // GET: api/v1/projects/{projectId}/attachedEmployees
        [HttpGet("{projectId}/attachedEmployees")]
        public async Task<ActionResult<UserInfoViewModel>> GetEmployeesAttachedToProject(int projectId)
        {
            _projectsClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));
            _identityClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(HttpContext.Request.Headers.Authorization));

            var employyesAttachedToProjectRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _projectsClient.BaseAddress + $"api/v1/employeesAttachedToProjects/byProject/{projectId}");

            var employyesAttachedToProjectResponse = await _projectsClient.SendAsync(employyesAttachedToProjectRequest);

            if (!employyesAttachedToProjectResponse.IsSuccessStatusCode)
            {
                return GetActionResultByStatusCode((int)employyesAttachedToProjectResponse.StatusCode);
            }

            if ((int)employyesAttachedToProjectResponse.StatusCode == 204)
            {
                return NoContent();
            }

            var employyesAttachedToProjectList = JsonSerializer.Deserialize<List<EmployeesAttachedToProjectsViewModel>>(await employyesAttachedToProjectResponse.Content.ReadAsStringAsync());

            if(employyesAttachedToProjectList == null || !employyesAttachedToProjectList.Any())
            {
                return NotFound();
            }

            var employees = new List<UserInfoViewModel>();

            foreach(var employee in employyesAttachedToProjectList)
            {
                var userInfoRequest = new HttpRequestMessage(
                HttpMethod.Get,
                    _projectsClient.BaseAddress + $"api/v1/userInfo/{employee.EmployeeId}");

                var userInfoResponse = await _projectsClient.SendAsync(userInfoRequest);

                if (!userInfoResponse.IsSuccessStatusCode)
                {
                    return GetActionResultByStatusCode((int)userInfoResponse.StatusCode);
                }

                if ((int)userInfoResponse.StatusCode == 204)
                {
                    return NoContent();
                }

                var userInfo = JsonSerializer.Deserialize<UserInfoViewModel>(await userInfoResponse.Content.ReadAsStringAsync());

                if(userInfo != null) 
                {
                    employees.Add(userInfo);
                }

            }

            return Ok(employees);

        }

    }
}
