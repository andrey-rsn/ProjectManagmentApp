using Microsoft.Extensions.Options;
using PMA_IdentityService.Constants;
using PMA_IdentityService.Models;
using PMA_IdentityService.Services;

namespace PMA_IdentityService.Data
{
    public class DatabaseSeed
    {
        public static async Task SeedAsync(ApplicationDbContext appDbContext, ILogger<ApplicationDbContext> logger, IOptions<SecretKeys> PasswordKeys)
        {
            if(!appDbContext.Positions.Any())
            {
                appDbContext.Positions.AddRange(GetPreconfiguredPositions());
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!appDbContext.Users.Any())
            {
                appDbContext.Users.AddRange(GetPreconfiguredUsers(PasswordKeys.Value.localKey));
                await appDbContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IEnumerable<Position> GetPreconfiguredPositions()
        {
            return new List<Position>
            {
                new Position
                {
                    Position_Id = 1,
                    PositionName = "Проектный менеджер",
                },
                new Position
                {
                    Position_Id = 2,
                    PositionName = "Разработчик",
                },
                new Position
                {
                    Position_Id = 3,
                    PositionName = "Тестировщик",
                }
            };
        }

        private static IEnumerable<User> GetPreconfiguredUsers(string passwordKey)
        {
            return new List<User>
            {
                new User
                {
                    User_Id = 1,
                    FirstName = "Andrey",
                    SecondName = "Korovay",
                    Patronymic = "Alexcandrovich",
                    Email = "korowai98.ag@gmail.com",
                    Login = "Admin",
                    Password = HashService.Encrypt("admin",passwordKey),
                    Role = UserRoles.PM,
                    Position_Id = 1
                }
            };
        }
    }
}
