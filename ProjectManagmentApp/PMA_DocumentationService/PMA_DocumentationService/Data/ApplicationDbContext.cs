using Microsoft.EntityFrameworkCore;
using PMA_DocumentationService.Models;

namespace PMA_DocumentationService.Data
{
    public class ApplicationDbContext: DbContext
    { 
        public DbSet<Document> Documents { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
