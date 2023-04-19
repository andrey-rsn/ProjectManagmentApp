using Microsoft.EntityFrameworkCore;
using PMA_TasksService.Models;

namespace PMA_TasksService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TaskComments> TaskComments { get; set; }
        public DbSet<UserTaskStatus> UserTaskStatuses { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTaskStatus>().HasData(new UserTaskStatus
            {
                userTaskStatusId= 1,
                statusName="Новая"
            });

            modelBuilder.Entity<UserTaskStatus>().HasData(new UserTaskStatus
            {
                userTaskStatusId = 2,
                statusName = "В работе"
            });

            modelBuilder.Entity<UserTaskStatus>().HasData(new UserTaskStatus
            {
                userTaskStatusId = 3,
                statusName = "Выполнена"
            });

            modelBuilder.Entity<UserTaskStatus>().HasData(new UserTaskStatus
            {
                userTaskStatusId = 4,
                statusName = "Завершена"
            });

            modelBuilder.Entity<UserTask>().HasData(new UserTask
            {
                taskId=1,
                userTaskStatusId=1,
                assignedUserId=1,
                changeDate=DateTime.UtcNow,
                description="Тестовая задача",
                priority=1,
                taskName="Тестовая задача 1",
                changedByUserId=1
            });

            modelBuilder.Entity<UserTask>().HasData(new UserTask
            {
                taskId = 2,
                userTaskStatusId = 1,
                assignedUserId = 1,
                changeDate = DateTime.UtcNow,
                description = "Тестовая задача 2",
                priority = 1,
                taskName = "Тестовая задача 2",
                changedByUserId = 1
            });

        }
    }
}
