using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMA_IdentityService.Data;
using PMA_IdentityService.Models;
using PMA_IdentityService.Models.DTOs;

namespace PMA_IdentityService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task Add(UserDTO Entity)
        {
            var User = _mapper.Map<User>(Entity);

            _dbContext.Users.Add(User);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<UserDTO> Entity)
        {
            var Users = _mapper.Map<IEnumerable<User>>(Entity);

            _dbContext.Users.AddRange(Users);

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

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var Users = await _dbContext.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(Users);
        }

        public async Task<UserDTO> GetById(int Id)
        {
            var User = await _dbContext.Users.FindAsync(Id);

            return _mapper.Map<UserDTO>(User);
        }

        public async Task Update(UserDTO Entity)
        {
            var User = _mapper.Map<User>(Entity);

            _dbContext.Update(User);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDTO> GetByLogin(string Login)
        {
            var User = await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email == Login);

            return _mapper.Map<UserDTO>(User);
        }
    }
}
