using Microsoft.EntityFrameworkCore;
using PMA_IdentityService.Data;
using PMA_IdentityService.Models;

namespace PMA_IdentityService.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(User Entity)
        {
            _dbContext.Users.Add(Entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<User> Entity)
        {
            _dbContext.Users.AddRange(Entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int Id)
        {
            var User = await _dbContext.Users.FindAsync(Id);

            if(User != null)
            {
                _dbContext.Users.Remove(User); 
                _dbContext.SaveChanges();    
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int Id)
        {
            return await _dbContext.Users.FindAsync(Id);
        }

        public async Task Update(User Entity)
        {
            _dbContext.Update(Entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
