namespace PMA_AnalyticsService.Services.TasksAnalyticsService
{
    public interface ITasksAnalyticsService
    {
        Task<int[]> GetTasksAnalyticsByProject(int projectId);
    }
}
