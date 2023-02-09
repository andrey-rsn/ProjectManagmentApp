using PMA_TasksService.Models;

namespace PMA_TasksService.Data
{
    public class DatabaseSeed
    {
        public static async Task SeedAsync(ApplicationDbContext appDbContext, ILogger<ApplicationDbContext> logger)
        {
            if (!appDbContext.UserTaskStatuses.Any())
            {
                appDbContext.UserTaskStatuses.AddRange(GetPreconfiguredUserTaskStatuses());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!appDbContext.UserTasks.Any())
            {
                appDbContext.UserTasks.AddRange(GetPreconfiguredUserTasks());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!appDbContext.Comments.Any())
            {
                appDbContext.Comments.AddRange(GetPreconfiguredComments());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!appDbContext.TaskComments.Any())
            {
                appDbContext.TaskComments.AddRange(GetPreconfiguredTaskComments());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

        }

        private static IEnumerable<TaskComments> GetPreconfiguredTaskComments()
        {
            return new List<TaskComments>
            {
                new TaskComments
                {
                    commentId= 1,
                    taskCommentsId= 1,
                    taskId= 1
                },
                new TaskComments
                {
                    commentId= 2,
                    taskCommentsId= 2,
                    taskId= 1
                }
            };
        }

        private static IEnumerable<Comment> GetPreconfiguredComments()
        {
            return new List<Comment>
            {
                new Comment
                {
                    authorId= 1,
                    commentId= 1,
                    commentText="Тестовый комментарий 1",
                    creationDate= DateTime.UtcNow
                },
                new Comment
                {
                    authorId= 1,
                    commentId= 2,
                    commentText="Тестовый комментарий 2",
                    creationDate= DateTime.UtcNow
                }
            };
        }

        private static IEnumerable<UserTaskStatus> GetPreconfiguredUserTaskStatuses()
        {
            return new List<UserTaskStatus>
            {
                new UserTaskStatus
                {
                    userTaskStatusId= 1,
                    statusName= "Новая"
                },
                new UserTaskStatus
                {
                    userTaskStatusId= 2,
                    statusName= "В работе"
                },
                new UserTaskStatus
                {
                    userTaskStatusId= 3,
                    statusName= "Выполнена"
                },
                new UserTaskStatus
                {
                    userTaskStatusId= 4,
                    statusName= "Завершена"
                }
            };
        }

        private static IEnumerable<UserTask> GetPreconfiguredUserTasks()
        {
            return new List<UserTask>
            {
                new UserTask
                {
                    taskId = 1,
                    userTaskStatusId= 1,
                    assignedUserId= 1,
                    changeDate= DateTime.UtcNow,
                    description="Тестовая задача, протестировать задачу.",
                    priority=2,
                    taskName="Тестовая задача"
                }
            };
        }
    }
}
