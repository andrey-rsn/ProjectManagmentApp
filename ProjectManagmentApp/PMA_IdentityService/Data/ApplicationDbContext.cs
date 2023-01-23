using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PMA_IdentityService.Constants;
using PMA_IdentityService.Models;
using PMA_IdentityService.Services;
using System.Text;

namespace PMA_IdentityService.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _passwordKey;
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<SecretKeys> PasswordKeys) : base(options)                                                         
        {
            _passwordKey = PasswordKeys.Value.localKey;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Position>().HasData(new Position
            {
                Position_Id = 1,
                PositionName = "Проектный менеджер",
            });

            modelBuilder.Entity<Position>().HasData(new Position
            {
                Position_Id = 2,
                PositionName = "Разработчик",
            });

            modelBuilder.Entity<Position>().HasData(new Position
            {
                Position_Id = 3,
                PositionName = "Тестировщик",
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                User_Id = 1,
                FirstName = "Andrey",
                SecondName = "Korovay",
                Patronymic = "Alexcandrovich",
                Email = "korowai98.ag@gmail.com",
                Login = "Admin",
                Password = HashService.Encrypt("admin",_passwordKey),
                Role = UserRoles.PM,
                Position_Id = 1
            });
        }
    }
}
