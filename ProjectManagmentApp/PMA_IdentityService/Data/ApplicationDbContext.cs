using Microsoft.EntityFrameworkCore;
using PMA_IdentityService.Models;

namespace PMA_IdentityService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
    }
}
