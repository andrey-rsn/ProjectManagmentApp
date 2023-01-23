using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PMA_WorkTimeService.Models;

namespace PMA_WorkTimeService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserWorkTime> UsersWorkTime { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
