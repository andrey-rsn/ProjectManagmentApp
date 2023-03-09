using Microsoft.Extensions.Options;
using PMA_WorkTimeService.Models;

namespace PMA_WorkTimeService.Data
{
    public class DatabaseSeed
    {
        public static async Task SeedAsync(ApplicationDbContext appDbContext, ILogger<ApplicationDbContext> logger)
        {
            if (!appDbContext.UsersWorkTime.Any())
            {
                appDbContext.UsersWorkTime.AddRange(GetPreconfiguredWorkTimes());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

        }

        private static IEnumerable<UserWorkTime> GetPreconfiguredWorkTimes()
        {
            return new List<UserWorkTime>
            {
                new UserWorkTime
                {
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddHours(1),
                    UserId= 1,
                    UserWorkTimeId=1
                }
            };
        }
    }
}
