using PMA_ProjectsService.Models;

namespace PMA_ProjectsService.Data
{
    public class DatabaseSeed
    {
        public static async Task SeedAsync(ApplicationDbContext appDbContext, ILogger<ApplicationDbContext> logger)
        {
            if (!appDbContext.Projects.Any())
            {
                appDbContext.Projects.AddRange(GetPreconfiguredProjects());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!appDbContext.EmployeesAttachedToProjects.Any())
            {
                appDbContext.EmployeesAttachedToProjects.AddRange(GetPreconfiguredEmployeesAttachedToProjects());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

        }

        private static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    Name= "Test project",
                    Description ="Test project for testing"
                },
                new Project
                {
                    ProjectId = 2,
                    Name = "Project Managment App",
                    Description = "Project Managment App description"
                }

            };
        }

        private static IEnumerable<EmployeesAttachedToProjectsModel> GetPreconfiguredEmployeesAttachedToProjects()
        {
            return new List<EmployeesAttachedToProjectsModel>
            {
                new EmployeesAttachedToProjectsModel
                {
                    EmployeeId= 1,
                    EmployeesAttachedToProjectsId= 1,
                    ProjectId= 1,
                },
                new EmployeesAttachedToProjectsModel
                {
                    EmployeeId = 1,
                    EmployeesAttachedToProjectsId = 2,
                    ProjectId = 2,
                }

            };
        }
    }
}
