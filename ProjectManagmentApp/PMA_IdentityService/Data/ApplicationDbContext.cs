using Microsoft.EntityFrameworkCore;
using PMA_IdentityService.Constants;
using PMA_IdentityService.Models;

namespace PMA_IdentityService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)                                                         
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.Entity<User>().HasData(new User
            {
                User_Id = 1,
                FirstName="Andrey",
                SecondName="Korovay",
                Patronymic="Alexcandrovich",
                Email="korowai98.ag@gmail.com",
                Password="admin",
                Role=UserRoles.PM
            });
        }
    }
}
