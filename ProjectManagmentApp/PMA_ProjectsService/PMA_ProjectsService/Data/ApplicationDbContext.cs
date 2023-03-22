using Microsoft.EntityFrameworkCore;
using PMA_ProjectsService.Models;

namespace PMA_ProjectsService.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeesAttachedToProjectsModel> EmployeesAttachedToProjects { get; set; }
        public DbSet<ProjectsTasksModel> ProjectsTasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasData(new Project
            {
                ProjectId = 1,
                Name= "Test project",
                Description ="Test project for testing"
            });

            modelBuilder.Entity<Project>().HasData(new Project
            {
                ProjectId = 2,
                Name = "Project Managment App",
                Description = "Project Managment App description"
            });

            modelBuilder.Entity<EmployeesAttachedToProjectsModel>().HasData(new EmployeesAttachedToProjectsModel
            {
                EmployeeId= 1,
                EmployeesAttachedToProjectsId= 1,
                ProjectId= 1,
            });

            modelBuilder.Entity<EmployeesAttachedToProjectsModel>().HasData(new EmployeesAttachedToProjectsModel
            {
                EmployeeId = 1,
                EmployeesAttachedToProjectsId = 2,
                ProjectId = 2,
            });

            modelBuilder.Entity<ProjectsTasksModel>().HasData(new ProjectsTasksModel
            {
                ProjectId= 1,
                ProjectsTasksId= 1,
                TaskId=1
            });

            modelBuilder.Entity<ProjectsTasksModel>().HasData(new ProjectsTasksModel
            {
                ProjectId = 1,
                ProjectsTasksId = 2,
                TaskId = 2
            });

        }
    }
}
