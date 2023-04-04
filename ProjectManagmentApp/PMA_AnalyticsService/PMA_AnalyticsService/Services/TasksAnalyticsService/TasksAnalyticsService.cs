using PMA_AnalyticsService.Constants;
using PMA_AnalyticsService.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace PMA_AnalyticsService.Services.TasksAnalyticsService
{
    public class TasksAnalyticsService : ITasksAnalyticsService
    {
        private readonly HttpClient _sagaClient;
        private readonly HttpContext _httpContext;
        public TasksAnalyticsService(IHttpClientFactory httpClientFactory, HttpContext httpContext)
        {
            _sagaClient = httpClientFactory.CreateClient("sagaServiceClient");
            _httpContext = httpContext;
        }
        public async Task<int[]> GetTasksAnalyticsByProject(int projectId)
        {
            _sagaClient.DefaultRequestHeaders.Add("Authorization", Convert.ToString(_httpContext.Request.Headers.Authorization));

            

            var projectTasksRequest = new HttpRequestMessage(
            HttpMethod.Get,
                    _sagaClient.BaseAddress + $"api/v1/userTask/byProject?projectId={projectId}&limit={10000}");


            var projectTasksResponse = await _sagaClient.SendAsync(projectTasksRequest);

            if (!projectTasksResponse.IsSuccessStatusCode || projectTasksResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new int[0];
            }

            var projectTasks = JsonSerializer.Deserialize<List<UserTaskViewModel>>(await projectTasksResponse.Content.ReadAsStringAsync());

            if(projectTasks == null || projectTasks.Count == 0)
            {
                return new int[0];
            }

            var analyticsArray = CreateTemplateArray();

            foreach (TaskStatusEnum status in Enum.GetValues(typeof(TaskStatusEnum)))
            {
                analyticsArray[(int)status-1] = projectTasks.Where(t => t.statusId == (int)status).Count();
            }

            return analyticsArray;
        }

        private int[] CreateTemplateArray()
        {
            return new int[] { 0, 0, 0, 0 };
        }
    }
}
